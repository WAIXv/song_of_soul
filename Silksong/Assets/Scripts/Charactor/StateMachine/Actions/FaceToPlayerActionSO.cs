using Charactor;
using Charactor.Knight_Boss;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FaceToPlayerAction", menuName = "State Machines/Actions/Face To Player Action")]
public class FaceToPlayerActionSO : StateActionSO
{
	public StateAction.SpecificMoment moment;
	protected override StateAction CreateAction() => new FaceToPlayerAction();
}

public class FaceToPlayerAction : StateAction
{
	private Knight_Boss _knightBoss;
	protected new FaceToPlayerActionSO OriginSO => (FaceToPlayerActionSO)base.OriginSO;

	public override void Awake(UOP1.StateMachine.StateMachine stateMachine)
	{
		_knightBoss = stateMachine.GetComponent<Knight_Boss>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
	{
		
	}
	
	public override void OnStateExit()
	{
	}
}
