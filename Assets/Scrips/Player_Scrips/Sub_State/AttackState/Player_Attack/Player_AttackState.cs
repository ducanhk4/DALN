using UnityEngine;

public class Player_AttackState : PlayerState
{
    protected bool comboQueued;
    protected bool attackFinished;

    public Player_AttackState(PlayerController player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        comboQueued = false;
        attackFinished = false;

        // Reset trigger animation
        player.anim.ResetTrigger(animBoolName);
        player.anim.SetTrigger(animBoolName);

        player.rb.velocity = Vector2.zero; // đứng yên khi bắt đầu
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (attackFinished)
        {
            if (comboQueued)
                GoToNextCombo();
            else
                stateMachine.ChangeState(player.idleState);
        }
    }

    public virtual void AnimationFinishTrigger()
    {
        attackFinished = true;
    }



    public void QueueCombo()
    {
        comboQueued = true;
    }

    protected virtual void GoToNextCombo() { }
}
