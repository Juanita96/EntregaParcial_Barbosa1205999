using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isNearChest = false;

    void Update()
    {
        if (isNearChest && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerInventory.hasKey)
            {
                Debug.Log("¡Cofre abierto!");

            }
            else
            {
                Debug.Log("Necesitas una llave para abrir este cofre.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearChest = true;
            Debug.Log("Presiona E para abrir el cofre");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearChest = false;
        }
    }
}
