using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected string animBoolName;
    protected float stateTime;

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.animBoolName = animBoolName;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        stateTime = Time.time;
        player.anim.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
    }
    public virtual void PhysicsUpdate()
    {
    }
}
