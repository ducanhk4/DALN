using UnityEngine;
public class Player_Attack1State : Player_AttackState
{
    public Player_Attack1State(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine, "attack1") { }

    protected override void GoToNextCombo()
    {
        stateMachine.ChangeState(player.attack2State);
    }

}


