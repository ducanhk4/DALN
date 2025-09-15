using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SlideState : PlayerState
{
    public Player_SlideState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.isGrounded && player.rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.xInput > 0 && !player.isFacingRight || player.xInput < 0 && player.isFacingRight)
        {
            player.Flip();
        }
    }
}
