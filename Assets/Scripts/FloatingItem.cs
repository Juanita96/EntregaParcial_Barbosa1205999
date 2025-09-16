using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float amplitude = 0.2f;   // altura del movimiento
    public float frequency = 2f;     // velocidad de la oscilación

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // guardamos la posición inicial
    }

    void Update()
    {
        // Movimiento senoidal en Y
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
