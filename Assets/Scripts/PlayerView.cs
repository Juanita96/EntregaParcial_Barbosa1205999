using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator; // para poder modificar desde unity

    //private solo permite acceso en este script
    //public peromite que otros scrip usen info de otros scripts
    private PlayerMovement movement;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        animator.SetBool("isRunning", movement.isMoving);
    }
}
