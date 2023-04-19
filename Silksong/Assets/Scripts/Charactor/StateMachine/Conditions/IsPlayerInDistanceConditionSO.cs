using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using DetectMethod = IsPlayerInDistanceConditionSO.DetectMethod;

[CreateAssetMenu(fileName = "IsPlayerInDistanceCondition", menuName = "State Machines/Conditions/Is Player In Distance Condition")]
public class IsPlayerInDistanceConditionSO : StateConditionSO
{
	public DetectMethod detectMethod;
	public float minDistance;
	public float maxDistance;
	protected override Condition CreateCondition() => new IsPlayerInDistanceCondition(minDistance,maxDistance,detectMethod);
	
	public enum DetectMethod
	{
		Horizontal,
		Vertical,
		Range,
	}
}

public class IsPlayerInDistanceCondition : Condition
{
	private Transform _playerTrans;
	private Transform _localTrans;
	private IsPlayerInDistanceConditionSO _originSO => (IsPlayerInDistanceConditionSO)base.OriginSO;
	private float _minDistance;
	private float _maxDistance;
	private DetectMethod _detectMethod;
	
	public IsPlayerInDistanceCondition(float minDistance,float maxDistance,DetectMethod detectMethod)
	{
		_minDistance = minDistance;
		_maxDistance = maxDistance;
		_detectMethod = detectMethod;
	}
	
	protected override bool Statement()
	{
		var target = _playerTrans.position;
		var local = _localTrans.position;
		float curDis;
		switch (_detectMethod)
		{
			case DetectMethod.Horizontal:
				if (Mathf.Abs(target.y - local.y) > 3f) return false;
				curDis = Mathf.Abs(target.x - local.x);
				break;
			case DetectMethod.Vertical:
				if (Mathf.Abs(target.x - local.x) > 3f) return false;
				curDis = Mathf.Abs(target.y - local.y);
				break;
			case DetectMethod.Range:
				curDis = Vector2.Distance(target, local);
				break;
			default:
				curDis = 0f;
				break;
		}

		return curDis >= _minDistance && curDis <= _maxDistance;
	}
	
	public override void Awake(UOP1.StateMachine.StateMachine stateMachine)
	{
		_playerTrans = PlayerController.Instance.transform;
		_localTrans = stateMachine.transform;
	}
}
