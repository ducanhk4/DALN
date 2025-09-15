using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SlideState : PlayerState
{
    public Player_SlideState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isSliding = true;
        player.playerCollider.size = player.slideSize;
        player.playerCollider.offset = player.slideOffset;
        if (player.isFacingRight)
            player.rb.velocity = new Vector2(player.slideSpeed, player.rb.velocity.y);
        else
            player.rb.velocity = new Vector2(-player.slideSpeed, player.rb.velocity.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.isFacingRight)
            player.rb.velocity = new Vector2(player.slideSpeed, player.rb.velocity.y);
        else
            player.rb.velocity = new Vector2(-player.slideSpeed, player.rb.velocity.y);

        // Kiểm tra xem thời gian trượt đã hết chưa
        if (Time.time >= stateTime + player.slideDuration)
        {
            if (player.isCrouching) // isCrouching của bạn là một raycast hướng lên trên để kiểm tra vật cản
            {
                stateMachine.ChangeState(player.crouchState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.isSliding = false;
        player.playerCollider.size = player.normalSize;
        player.playerCollider.offset = player.normalOffset;

        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
    }
}