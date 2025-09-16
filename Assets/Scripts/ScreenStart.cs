using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenStart : MonoBehaviour
{
    public static ScreenStart Instance;

    [Header("Referencias")]
    public GameObject introPanel;        // Panel negro con texto
    public PlayerMovement playerMovement;
    public PlayerDash playerDash;
    public PlayerView playerView;        // Opcional si querés controlar animaciones
    public Animator playerAnimator;      // Animator del personaje (para pausar animaciones)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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

    private void Update()
    {
        // Detecta Enter para iniciar el juego
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            StartGame();
        }
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
    }
}