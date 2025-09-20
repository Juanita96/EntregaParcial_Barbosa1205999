using UnityEngine;

public class KeySound : MonoBehaviour
{
   private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayKeySound()
    {
        audioSource.Play();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            PlayKeySound();
        }
    }
}
