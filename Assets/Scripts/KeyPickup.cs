using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] private InputActionReference KeyPickup; // tecla para recoger
    [SerializeField] private PlayerInventory playerInventory; // referencia al Player

    private bool isNearKey = false;
    private GameObject keyObject;

    private void OnEnable()
    {
        KeyPickup.action.performed += HandleKeyPickupInput;
        KeyPickup.action.Enable();
    }

    private void OnDisable()
    {
        KeyPickup.action.performed -= HandleKeyPickupInput;
        KeyPickup.action.Disable();
    }

    private void HandleKeyPickupInput(InputAction.CallbackContext context)
    {
        if (isNearKey && playerInventory != null)
        {
            playerInventory.hasKey = true; // guardamos la llave en el inventario
            Destroy(keyObject);
            Debug.Log("Llave recogida!");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            isNearKey = true;
            keyObject = other.gameObject;
            Debug.Log("Jugador cerca de la llave");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            isNearKey = false;
            keyObject = null;
        }
    }
}
