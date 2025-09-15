using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player_FallState : PlayerState
{
    public Player_FallState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.rb.velocity = new Vector2(player.xInput * player.moveSpeed, player.rb.velocity.y);
        if (player.isGrounded && player.rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.xInput > 0 && !player.isFacingRight || player.xInput < 0 && player.isFacingRight)
        {
            player.Flip();
        }

        if (player.isTouchingWall && !player.isGrounded)
        {
            stateMachine.ChangeState(player.grabState);
        }
    }
}
