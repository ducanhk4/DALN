using UnityEngine;
public class Player_Attack2State : Player_AttackState
{
    public Player_Attack2State(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine, "attack2") { }

    protected override void GoToNextCombo()
    {
        stateMachine.ChangeState(player.attack3State);
    }

}