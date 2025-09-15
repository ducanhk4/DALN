using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CrouchState : PlayerState
{
    public Player_CrouchState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerCollider.size = player.slideSize;
        player.playerCollider.offset = player.slideOffset;
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.xInput != 0)
        {
            stateMachine.ChangeState(player.crouchMoveState);
        }
        else if (!player.isCrouching)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.playerCollider.size = player.normalSize;
        player.playerCollider.offset = player.normalOffset;
    }
}
