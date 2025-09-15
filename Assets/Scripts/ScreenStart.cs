using UnityEngine;

public class IntroPanel : MonoBehaviour
{
    void Start()
    {
        // No pausamos el tiempo, solo bloqueamos input
        PlayerMovement.gameStarted = false;
    }

    void Update()
    {
        if (!PlayerMovement.gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);
            PlayerMovement.gameStarted = true; 
        }
    }
}
