using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CrouchMoveState : PlayerState
{
    public Player_CrouchMoveState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerCollider.size = player.slideSize;
        player.playerCollider.offset = player.slideOffset;
        player.rb.velocity = new Vector2(player.xInput * player.moveSpeed, player.rb.velocity.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.rb.velocity = new Vector2(player.xInput * player.moveSpeed, player.rb.velocity.y);
        if (player.xInput == 0)
        {
            stateMachine.ChangeState(player.crouchState);
        }
        else if (!player.isCrouching)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.isFacingRight && player.xInput < 0)
        {
            player.Flip();
        }
        else if (!player.isFacingRight && player.xInput > 0)
        {
            player.Flip();
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.playerCollider.size = player.normalSize;
        player.playerCollider.offset = player.normalOffset;
    }
}
