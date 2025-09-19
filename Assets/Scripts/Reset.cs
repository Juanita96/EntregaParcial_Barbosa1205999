using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] private InputActionReference resetAction; // Acci�n asociada a la tecla R

    private Vector3 startPosition;

    private void Awake()
    {
        // Guardamos la posici�n inicial al inicio del juego
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        resetAction.action.Enable();
        resetAction.action.performed += OnResetPerformed;
    }

    private void OnDisable()
    {
        resetAction.action.performed -= OnResetPerformed;
        resetAction.action.Disable();
    }

    private void OnResetPerformed(InputAction.CallbackContext context)
    {
        // Reiniciamos la posici�n del jugador
        transform.position = startPosition;

        // Si tu personaje usa Rigidbody2D o Rigidbody, conviene resetear la velocidad tambi�n
        if (TryGetComponent<Rigidbody2D>(out var rb2d))
        {
            rb2d.linearVelocity = Vector2.zero;
        }
        else if (TryGetComponent<Rigidbody>(out var rb3d))
        {
            rb3d.linearVelocity = Vector3.zero;
        }
    }
}
