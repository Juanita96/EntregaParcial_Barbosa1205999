using UnityEngine;
using UnityEngine.InputSystem; // si querés interacción con botón

public class Chest : MonoBehaviour
{
    private bool playerNear = false;
    private PlayerInventory playerInv;

    public Animator animator; // opcional, si tenés animación de abrir cofre

    void Update()
    {
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (playerInv != null && playerInv.hasKey)
            {
                if (animator != null) animator.SetTrigger("Open");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerInv = other.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            playerInv = null;
        }
    }
}
