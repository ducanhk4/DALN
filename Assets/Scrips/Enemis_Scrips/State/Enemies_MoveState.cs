using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_MoveState : Enemies_State
{
    public D_Move StateData { get; private set; }

    private bool isDetectingWall;
    private bool isDetectingLedge;

    public Enemies_MoveState(Entity enemy, Enemies_StateMachine stateMachine, D_Move StateData, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
        this.StateData = StateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.RB.velocity = new Vector2(entity.FacingDirection * StateData.moveSpeed, entity.RB.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isDetectingWall = entity.CheckForWall();
        isDetectingLedge = !entity.CheckForLedge();


        if (isDetectingWall || isDetectingLedge)
        {
            stateMachine.ChangeState(entity.IdleState);
        }

        if (entity.isPlayer)
        {
            stateMachine.ChangeState(entity.DetectingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.RB.velocity = new Vector2(entity.FacingDirection * StateData.moveSpeed, entity.RB.velocity.y);
    }
}