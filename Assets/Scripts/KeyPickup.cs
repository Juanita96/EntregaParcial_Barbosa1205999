using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    private bool isNearKey = false;
    private GameObject keyObject;

    void Update()
    {
        if (isNearKey && Input.GetKeyDown(KeyCode.E))
        {
            // "Agarrar" la llave
            Destroy(keyObject); // destruye la llave de la escena

            // Podés guardar en una variable global o GameManager
            PlayerInventory.hasKey = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            isNearKey = true;
            keyObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            isNearKey = false;
            keyObject = null;
        }
    }
}
