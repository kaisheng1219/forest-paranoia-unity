using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject Player;
    private bool notified;
    private AudioSource audioSource;
    private AudioSource weatherSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        weatherSource = GameObject.Find("Weather").GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameStateManager.GameState = GameStateManager.State.Won;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 100f && !notified)
        {
            Player.GetComponent<PlayerNotification>().NotifEscapeIsAround();
            notified = true;
        }
    }
}
