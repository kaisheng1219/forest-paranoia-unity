using UnityEngine;

public class PlayerNotification : MonoBehaviour
{
    public AudioClip CabinClip, EscapeClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NotifCabinIsAround()
    {
        audioSource.PlayOneShot(CabinClip);
    }

    public void NotifEscapeIsAround()
    {
        audioSource.PlayOneShot(EscapeClip);
    }
}
