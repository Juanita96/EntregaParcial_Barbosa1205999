using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class KeyPickupUI : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    private bool playerNear = false;
    private PlayerInventory playerInv;

    void Start()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false); // arranca oculto
    }

    void Update()
    {
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            playerInv.PickupKey();

            if (promptText != null)
                promptText.gameObject.SetActive(false); // texto desaparece al recoger

            Destroy(gameObject); // destruir llave
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerInv = other.GetComponent<PlayerInventory>();
            if (promptText != null)
                promptText.gameObject.SetActive(true); // mostrar texto
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
