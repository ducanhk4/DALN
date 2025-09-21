using UnityEngine;
public class Player_Attack3State : Player_AttackState
{
    public Player_Attack3State(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine, "attack3") { }

    protected override void GoToNextCombo()
    {
        // Hết combo → quay lại Idle
        stateMachine.ChangeState(player.idleState);
    }
}