using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class Player_IdleState : PlayerState
{
    public Player_IdleState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }

        else if (Input.GetButtonDown("Jump") && player.isGrounded)
        {
            stateMachine.ChangeState(player.jumpState);
        }

        if (!player.isGrounded && player.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

}
