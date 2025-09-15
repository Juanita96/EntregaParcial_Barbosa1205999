using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private InputActionReference dashAction;

    [SerializeField] private float dashDistance = 0.6f;
    [SerializeField] private float dashDuration = 0.2f;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float dashCooldown = 1f;
    private float nextDashTime = 0f;

    private Vector2 dashDirection;

    private Rigidbody2D rb;

    public bool IsDashing = false;
    void Start()
    {
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

        if (playerMovement != null)
            playerMovement.enabled = false;

        Vector2 startVelocity = rb.linearVelocity;
        rb.linearVelocity = dashDirection.normalized * (dashDistance / dashDuration);

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = startVelocity;
        
        if (playerMovement != null)
            playerMovement.enabled = true;
        IsDashing = false;
    }

    void Update()
    {
        Vector2 dirDash;

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
