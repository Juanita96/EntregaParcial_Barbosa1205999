using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory; // referencia al Player
    [SerializeField] private InputActionReference OpenChest;   // tecla para abrir el cofre

    private Animator animator;
    private bool isNearChest = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OpenChest.action.performed += HandleOpenChestInput;
        OpenChest.action.Enable();
    }

    private void OnDisable()
    {
        OpenChest.action.performed -= HandleOpenChestInput;
        OpenChest.action.Disable();
    }

    private void HandleOpenChestInput(InputAction.CallbackContext context)
    {
        if (isNearChest && playerInventory != null && playerInventory.hasKey)
        {
            animator.SetBool("chestOpen", true);
            Debug.Log("Cofre abierto!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isNearChest = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isNearChest = false;
    }
}
