using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator; // para poder modificar desde unity

    //private solo permite acceso en este script
    //public peromite que otros scrip usen info de otros scripts
    private PlayerMovement movement;
    private PlayerDash dash;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        dash = GetComponent<PlayerDash>();
    }

    void Update()
    {
        animator.SetBool("isRunning", movement.isMoving);
        animator.SetBool("isJumpping", movement.jumpOnGround);
        animator.SetBool("isGrounded", movement.isGrounded);
        animator.SetBool("isDashing", dash.IsDashing);
    }
}
