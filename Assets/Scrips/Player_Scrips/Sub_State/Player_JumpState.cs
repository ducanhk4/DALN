using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player_JumpState : PlayerState
{
    public Player_JumpState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.rb.velocity = new Vector2(player.xInput * player.moveSpeed, player.rb.velocity.y);

        if (player.isGrounded && player.rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (player.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
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
