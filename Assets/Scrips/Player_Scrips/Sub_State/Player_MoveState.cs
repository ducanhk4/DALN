using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_MoveState : PlayerState
{
    public Player_MoveState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = new Vector2(player.xInput * player.moveSpeed, player.rb.velocity.y);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.rb.velocity = new Vector2(player.xInput * player.moveSpeed, player.rb.velocity.y);
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            stateMachine.ChangeState(player.idleState);
            player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
        }
        if (player.xInput > 0 && !player.isFacingRight || player.xInput < 0 && player.isFacingRight)
        {
            player.Flip();
        }

        if (Input.GetButtonDown("Jump") && player.isGrounded)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (!player.isGrounded && player.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
