using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static bool gameStarted = false;

    [SerializeField] private InputActionReference moveAction;
    public Vector2 moveInput;
    public Vector2 lastMoveDirection = Vector2.right;

    [SerializeField] private InputActionReference jumpAction;
    private bool jumpInput;


    [SerializeField] private LayerMask groundLayer;

    RaycastHit2D hitFloor; //Variable de tipo bool que da un resultado de verdadero o falso
    public float rayDist = 2f;
    public bool isGrounded = false;

    public float moveSpeed = 7f;
    public float jumpForce = 7f;

    public bool isMoving => Mathf.Abs(moveInput.x) > 0.01f;

    public bool jumpOnGround = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerDash playerDash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerDash = GetComponent<PlayerDash>();

        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.performed += HandleJumpInput;

    }

    void HandleMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);

        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }
    }

    void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpOnGround = true;
        }
    } 

    void Update()
    {
        if (!gameStarted) return;

        if (playerDash != null && playerDash.IsDashing)
            return;

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        hitFloor = Physics2D.Raycast(transform.position, Vector2.down, rayDist, groundLayer);

        if(hitFloor.collider != null )
        {
            Debug.DrawRay(transform.position, Vector2.down* rayDist, Color.green);
            isGrounded = true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * rayDist, Color.red);
            isGrounded = false;
            jumpOnGround = false;
        }

        if (moveInput.x > 0.01f)
        {
            sr.flipX = false;
        }
        else if (moveInput.x < -0.01f)
        {
            sr.flipX = true;
        }
    }
}