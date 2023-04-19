using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalMoveAction", menuName = "State Machines/Actions/Horizontal Move Action")]
public class HorizontalMoveActionSO : StateActionSO
{
	protected override StateAction CreateAction() => new HorizontalMoveAction();
}

public class HorizontalMoveAction : StateAction
{
	protected new HorizontalMoveActionSO OriginSO => (HorizontalMoveActionSO)base.OriginSO;

	public override void Awake(UOP1.StateMachine.StateMachine stateMachine)
	{
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
