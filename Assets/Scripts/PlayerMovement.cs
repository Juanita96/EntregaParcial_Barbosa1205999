using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private InputActionReference moveAction;
    private Vector2 moveInput;

    [SerializeField] private InputActionReference jumpAction;
    private bool jumpInput;

    public float moveSpeed = 7f;
    public float jumpForce = 7f;

    public bool isMoving => Mathf.Abs(moveInput.x) > 0.01f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    bool isGrounded = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.performed += HandleJumpInput;
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
    }
}