using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies_State
{
    public Enemies_StateMachine stateMachine;
    public Entity entity;
    public float startTime;

    public string animBoolName;
    public Enemies_State(Entity entity, Enemies_StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        entity.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
    }
    public virtual void Exit()
    {
        entity.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
}
