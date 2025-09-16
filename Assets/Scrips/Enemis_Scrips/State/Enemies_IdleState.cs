using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_IdleState : Enemies_State
{
    public D_Idle StateData { get; private set; }
    private bool isIdleTimeOver;

    public Enemies_IdleState(Entity enemy, Enemies_StateMachine stateMachine, D_Idle StateData, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
        this.StateData = StateData;
    }

    public override void Enter()
    {
        base.Enter();
        isIdleTimeOver = false;
        entity.RB.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + StateData.idleDuration)
        {
            isIdleTimeOver = true;
        }

        if (isIdleTimeOver)
        {
            entity.Flip();
            stateMachine.ChangeState(entity.MoveState);
        }
        if (entity.isPlayer)
        {
            stateMachine.ChangeState(entity.DetectingState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}