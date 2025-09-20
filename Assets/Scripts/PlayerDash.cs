using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private InputActionReference dashAction;
    [SerializeField] private PlayerMovement playerMovement;

    // Dash parameters
    [SerializeField] private float dashDistance = 0.6f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
   
    private float nextDashTime = 0f;

    private Vector2 dashDirection; 

    private Rigidbody2D rb; 

    public bool IsDashing = false; // Bool to check if the player is dashing

    void Start()
    {
        PlayerMovement.gameStarted = true;
        dashAction.action.Enable();

        playerMovement = GetComponent<PlayerMovement>();

        rb = GetComponent<Rigidbody2D>();

        dashAction.action.performed += HandleDashInput;
    }

    void HandleDashInput(InputAction.CallbackContext context)
    {
        if (!IsDashing && Time.time >= nextDashTime)
        {
            StartCoroutine(Dash());
            nextDashTime = Time.time + dashCooldown;
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        IsDashing = true;

        // Disable player movement during dash
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Calculate dash velocity
        Vector2 startVelocity = rb.linearVelocity;
        rb.linearVelocity = dashDirection.normalized * (dashDistance / dashDuration);

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = startVelocity; // Restore original velocity

        // Reenable player movement after dash
        if (playerMovement != null)
            playerMovement.enabled = true;
        IsDashing = false;
    }

    void Update()
    {
        if (!PlayerMovement.gameStarted) return;

        Vector2 dirDash;

        // Determine dash direction based on last movement input
        if (playerMovement.moveInput != Vector2.zero)
        {
            dirDash = playerMovement.moveInput;
        }
        else
        {
            dirDash = playerMovement.lastMoveDirection;
        }

        if (Mathf.Abs(dirDash.x) > Mathf.Abs(dirDash.y))
        {
            if (dirDash.x > 0)
            {
                dashDirection = Vector2.right;
            }
            else
            {
                dashDirection = Vector2.left;
            }
        }
    }
}
