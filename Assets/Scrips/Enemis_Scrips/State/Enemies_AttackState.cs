using UnityEngine;

public class Enemies_AttackState : Enemies_State
{
    private float attackCooldown;

    public Enemies_AttackState(Entity entity, Enemies_StateMachine stateMachine, string animBoolName, float cooldown)
        : base(entity, stateMachine, animBoolName)
    {
        this.attackCooldown = cooldown;
    }

    public override void Enter()
    {
        base.Enter();
        entity.RB.velocity = Vector2.zero;
        startTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!entity.isPlayer)
        {
            stateMachine.ChangeState(entity.IdleState);
        }
        if (entity.isPlayer)
        {
            stateMachine.ChangeState(entity.DetectingState);
        }
        else if (Time.time >= startTime + attackCooldown)
        {
            startTime = Time.time;

        }

    }
}