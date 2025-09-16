using UnityEngine;

public class Enemies_DetectingState : Enemies_State
{
    private float detectDuration;

    public Enemies_DetectingState(Entity entity, Enemies_StateMachine stateMachine, string animBoolName, float timeToDetect)
        : base(entity, stateMachine, animBoolName)
    {
        this.detectDuration = timeToDetect;
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
        else if (Time.time >= startTime + detectDuration)
        {
            stateMachine.ChangeState(entity.AttackState);
            Debug.Log("Detecting done -> Attack!");
        }
    }
}