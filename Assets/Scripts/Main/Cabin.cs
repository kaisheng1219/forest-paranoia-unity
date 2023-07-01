using UnityEngine;

public class Cabin : MonoBehaviour
{
    [SerializeField] private AudioClip enterClip;

    public GameObject Player;
    private bool playerInside, notified;
    private AudioSource audioSource;
    private AudioSource weatherSource;

    private void Awake()
    {
        weatherSource = GameObject.Find("Weather").GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(enterClip);
            weatherSource.volume = 0.5f;
            GameStateManager.GameState = GameStateManager.State.Invisible;
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            weatherSource.volume = 1f;
            audioSource.PlayOneShot(enterClip);
            GameStateManager.GameState = GameStateManager.State.CanStartTapping;
            playerInside = false;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerInside)
        {
            transform.localScale += new Vector3(.1f, .1f, .1f) * -5 * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < 100f && !notified)
        {
            Player.GetComponent<PlayerNotification>().NotifCabinIsAround();
            notified = true;
        }
    }
}
