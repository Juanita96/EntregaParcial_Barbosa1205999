using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;

    public void PickupKey()
    {
        hasKey = true;
    }
}