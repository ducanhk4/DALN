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
    public Player_SlideState wallSlide { get; private set; }
    public float xInput { get; private set; }
    public bool isFacingRight = true;


    [Header("Player Stats")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public bool isGrounded => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    public bool isTouchingWall => Physics2D.Raycast(wallCheck.position, transform.right, 0.1f, groundLayer);
    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jump");
        fallState = new Player_FallState(this, stateMachine, "fall");
        grabState = new Player_GrabState(this, stateMachine, "grab");
        wallSlide = new Player_SlideState(this, stateMachine, "wallSlide");
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        stateMachine.CurrentState.LogicUpdate();
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * 0.1f);
    }
}
