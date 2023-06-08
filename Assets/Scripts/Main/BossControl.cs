using UnityEngine;

public class BossControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSpeech;
    [SerializeField] private GameObject InGameText;
    [SerializeField] private GameObject children;

    private AudioSource playerAudio;
    private bool canSayIntro;
    private float speed = 2f;

    void Start()
    {
        playerAudio = playerSpeech.GetComponent<AudioSource>();
        canSayIntro = true;
    }

    void Update()
    {
        if (GameStateManager.GameState == GameStateManager.State.Started)
        {
            if (!playerAudio.isPlaying && canSayIntro)
            {
                GetComponent<AudioSource>().Play();
                canSayIntro = false;
            }

            if (!playerAudio.isPlaying && !GetComponent<AudioSource>().isPlaying)
            {
                if (!children.GetComponent<AudioSource>().isPlaying)
                {
                    children.GetComponent<AudioSource>().Play();
                }
                InGameText.SetActive(true);
                
                Vector3 direction = player.transform.position - transform.position;
                transform.position += speed * Time.deltaTime * direction.normalized;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameStateManager.GameState = GameStateManager.State.Ended;
        }
    }
}
