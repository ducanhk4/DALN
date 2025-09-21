using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerStateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_GrabState grabState { get; private set; }
    public Player_WallSlideState wallSlide { get; private set; }
    public Player_SlideState slideState { get; private set; }
    public Player_CrouchState crouchState { get; private set; }
    public Player_CrouchMoveState crouchMoveState { get; private set; }
    public Player_Attack1State attack1State { get; private set; }
    public Player_Attack2State attack2State { get; private set; }
    public Player_Attack3State attack3State { get; private set; }
    public float xInput { get; private set; }
    public bool isFacingRight = true;
    // Trong PlayerController.cs
    public int facingDirection { get; private set; } = 1; // 1 = phải, -1 = trái
    public BoxCollider2D playerCollider;



    [Header("Player Stats")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;

    [Header("Slide Settings")]
    public float slideSpeed = 12f;
    public float slideDuration = 0.6f;
    public float slideStopDuration = 5f;
    public bool isSliding = false;
    [Header("Collider Settings")]
    public Vector2 normalSize;
    public Vector2 normalOffset;
    public Vector2 slideSize;
    public Vector2 slideOffset;


    [Header("Ground Check")]
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform crouchingCheck;
    public LayerMask groundLayer;
    public bool isGrounded => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    public bool isTouchingWall => Physics2D.Raycast(wallCheck.position, transform.right, 0.1f, groundLayer);
    public bool isCrouching => Physics2D.Raycast(crouchingCheck.position, Vector2.up, 0.1f, groundLayer);


    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jump");
        fallState = new Player_FallState(this, stateMachine, "fall");
        grabState = new Player_GrabState(this, stateMachine, "grab");
        wallSlide = new Player_WallSlideState(this, stateMachine, "wallSlide");
        slideState = new Player_SlideState(this, stateMachine, "slide");
        crouchState = new Player_CrouchState(this, stateMachine, "crouch");
        crouchMoveState = new Player_CrouchMoveState(this, stateMachine, "crouchMove");
        attack1State = new Player_Attack1State(this, stateMachine);
        attack2State = new Player_Attack2State(this, stateMachine);
        attack3State = new Player_Attack3State(this, stateMachine);

    }

    void Start()
    {
        stateMachine.Initialize(idleState);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        int count = Enum.GetValues(typeof(CombatState)).Length;
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isSliding)
        {
            stateMachine.ChangeState(slideState);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (stateMachine.CurrentState == idleState)
                stateMachine.ChangeState(attack1State);
            else if (stateMachine.CurrentState is Player_AttackState attackState)
                attackState.QueueCombo();
        }

        stateMachine.CurrentState.LogicUpdate();

    }


    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        facingDirection *= -1;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(crouchingCheck.position, crouchingCheck.position + transform.up * 0.1f);
    }

    public void AnimationFinishTrigger()
    {
        if (stateMachine.CurrentState is Player_AttackState attackState)
        {
            attackState.AnimationFinishTrigger();
        }
    }
    // Animation Event: bắt đầu dash nhẹ
    public void StartAttackMovement()
    {
        float dashSpeed = 1f; // tốc độ trượt
        rb.velocity = new Vector2(facingDirection * dashSpeed, 0f);
    }

    // Animation Event: dừng lại
    public void StopAttackMovement()
    {
        rb.velocity = Vector2.zero;
    }

}

public enum CombatState
{

}




