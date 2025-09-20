using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] private InputActionReference resetAction;

    private Vector3 startPosition;

    private void Awake()
    {
        // save player position at start 
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
        // player position reset
        transform.position = startPosition;

        // player velocity reset
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
