using UnityEngine;
using UnityEngine.InputSystem; // si usás el nuevo Input System

public class KeyPickup : MonoBehaviour
{
    private bool playerNear = false;
    private PlayerInventory playerInv;

    void Update()
    {
        // Si el jugador está cerca y presiona E, recoge la llave
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            playerInv.PickupKey();
            Destroy(gameObject); // la llave desaparece
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerInv = other.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            playerInv = null;
        }
    }
}
