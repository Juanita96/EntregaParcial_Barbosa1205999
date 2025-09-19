using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenStart : MonoBehaviour
{
    public static ScreenStart Instance;

    [Header("References")]
    public GameObject introPanel;        // Panel negro con texto
    public PlayerMovement playerMovement;
    public PlayerDash playerDash;
    public PlayerView playerView;        // Opcional si querés controlar animaciones
    public Animator playerAnimator;      // Animator del personaje (para pausar animaciones)

    [Header("Input System")]
    [SerializeField] private InputActionReference startAction; // Acción para comenzar el juego (ej: Enter)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        // Nos suscribimos al evento de InputAction
        if (startAction != null)
            startAction.action.performed += OnStartPressed;
    }

    private void OnDisable()
    {
        // Nos desuscribimos para evitar errores
        if (startAction != null)
            startAction.action.performed -= OnStartPressed;
    }

    private void Start()
    {
        // Muestra el panel y pausa todo
        introPanel.SetActive(true);

        // Bloqueamos inputs de movimiento y dash
        playerMovement.enabled = false;
        playerDash.enabled = false;

        // Pausamos físicas y animaciones
        Time.timeScale = 0f;
        if (playerAnimator != null)
            playerAnimator.speed = 0f; // pausa animaciones
    }

    private void OnStartPressed(InputAction.CallbackContext context)
    {
        StartGame();
    }

    private void StartGame()
    {
        introPanel.SetActive(false);

        // Desbloqueamos inputs
        playerMovement.enabled = true;
        playerDash.enabled = true;

        // Reanudamos físicas y animaciones
        Time.timeScale = 1f;
        if (playerAnimator != null)
            playerAnimator.speed = 1f;

        // Importante: desactivar el action después de usarlo
        if (startAction != null)
            startAction.action.Disable();
    }
}
