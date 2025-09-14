using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    private Vector2 moveInput;

    [SerializeField] private InputActionReference jumpAction;
    private bool jumpInput;

    [SerializeField] private InputActionReference dashAction;
    private bool dashInput;

    public float moveSpeed = 7f;
    public float jumpForce = 7f;
    public float dashSpeed = 14f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public bool isMoving => Mathf.Abs(moveInput.x) > 0.01f;

    public bool jumpOnGround = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float dashTime;
    private float dashCooldownTime;
    public bool isDashing = false;
    public bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.performed += HandleJumpInput;

        dashAction.action.performed += context => dashInput = true;
    }

    void HandleMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpOnGround = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            jumpOnGround = false;
        }
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (moveInput.x > 0.01f)
        {
            sr.flipX = false;
        }
        else if (moveInput.x < -0.01f)
        {
            sr.flipX = true;
        }
        if (dashInput && !isDashing && Time.time >= dashCooldownTime)
        {
            isDashing = true;
            dashTime = Time.time + dashDuration;
            dashCooldownTime = Time.time + dashCooldown;
        }
        if (isDashing)
        {
            if (Time.time < dashTime)
            {
                rb.linearVelocity = new Vector2(sr.flipX ? -dashSpeed : dashSpeed, rb.linearVelocity.y);
            }
            else
            {
                isDashing = false;
                dashInput = false;
            }
        }
    }
}