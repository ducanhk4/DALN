using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GrabState : PlayerState
{
    private float grabStartTime;
    private float grabDuration = 2f;
    public Player_GrabState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = Vector2.zero;
        player.rb.gravityScale = 0f;
        grabStartTime = Time.time;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (!player.isTouchingWall)
        {
            stateMachine.ChangeState(player.fallState);

        }
        if (Time.time - grabStartTime > grabDuration)
        {
            player.rb.gravityScale = 2f;
            player.rb.velocity = new Vector2(0f, -2f);
            stateMachine.ChangeState(player.wallSlide);
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = 2f;
    }


}
