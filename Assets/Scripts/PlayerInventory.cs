using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventario")]
    public bool hasKey = false;

    private void Start()
    {
        Debug.Log("PlayerInventory hasKey: " + hasKey);
    }
}
