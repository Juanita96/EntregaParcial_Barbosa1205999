using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    private bool isNearChest = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isNearChest && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerInventory.hasKey)
            {
                animator.SetBool("isOpen", true);

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
