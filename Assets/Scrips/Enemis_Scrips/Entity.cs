using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("State Machine Data")]
    public D_Idle IdleData;
    public D_Move MoveData;

    [Header("Transforms for Checks")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;

    [Header("Check Parameters")]
    [SerializeField] private float wallCheckDistance = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float playerCheckRadius = 0.5f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float TimeToDetect;
    public bool isPlayer => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer); // Đã di chuyển xuống đây

    public int FacingDirection { get; private set; }

    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    public Enemies_StateMachine StateMachine { get; private set; }
    public Enemies_IdleState IdleState { get; private set; }
    public Enemies_MoveState MoveState { get; private set; }
    public Enemies_DetectingState DetectingState { get; private set; }
    public Enemies_AttackState AttackState { get; private set; }

    public virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        StateMachine = new Enemies_StateMachine();

        IdleState = new Enemies_IdleState(this, StateMachine, IdleData, "idle");
        MoveState = new Enemies_MoveState(this, StateMachine, MoveData, "move");
        DetectingState = new Enemies_DetectingState(this, StateMachine, "detecting", TimeToDetect);
        AttackState = new Enemies_AttackState(this, StateMachine, "attack", 1f);
    }

    public virtual void Start()
    {
        FacingDirection = 1;
        StateMachine.Initialize(IdleState);
    }

    public virtual void Update()
    {
        StateMachine.currentState.LogicUpdate();
    }

    public void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    #region Check Methods
    public virtual bool CheckForWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, whatIsGround);
    }

    public virtual bool CheckForLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, wallCheckDistance * 2, whatIsGround);
    }
    #endregion

    #region Flip Method
    public virtual void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion

    private void OnDrawGizmos()
    {
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * 0.1f);
        }
        if (ledgeCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + transform.up * 0.1f);
        }
        if (playerCheck != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(playerCheck.position, playerCheckRadius);
        }
    }
}