using System.Collections;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSpeech;
    [SerializeField] private GameObject InGameText;
    [SerializeField] private GameObject children;

    public float RandomisedTempo = 0.5f;
    public float Speed;

    private AudioSource playerAudio;
    private PlayerInput playerInput;
    private bool canSayIntro;
    private Vector3 startingPosition;
    private float timeInSlowSpeed, randomisedTimeToChange;

    private const float initialSpeed = 7f;
    private const float maxSpeed = 60f;
    private const float slowSpeed = 1f;
    private const float normalSpeed = 7f;
    private const float humanTapSpeed_Fast = 0.07f;
    private const float humanTapSpeed_Slow = 1.5f;
    private const float tempoFastest = 0.5f;

    void Start()
    {
        playerAudio = playerSpeech.GetComponent<AudioSource>();
        playerInput = player.GetComponent<PlayerInput>();
        canSayIntro = true;
        startingPosition = transform.position;
    }

    void Update()
    {
        if (GameStateManager.GameState == GameStateManager.State.CanStartTapping)
        {
            if (!children.GetComponent<AudioSource>().isPlaying)
            {
                children.GetComponent<AudioSource>().Play();
            }
            InGameText.SetActive(true);

            Vector3 direction = player.transform.position - transform.position;
            Speed = Mathf.Clamp(Speed, slowSpeed, maxSpeed);
            transform.position += Speed * Time.deltaTime * direction.normalized;
        }
        else if (GameStateManager.GameState == GameStateManager.State.Started)
        {
            if (!playerAudio.isPlaying && canSayIntro)
            {
                GetComponent<AudioSource>().Play();
                canSayIntro = false;
            }
        } else if (GameStateManager.GameState == GameStateManager.State.Invisible)
        {
            Vector3 direction = player.transform.position - transform.position;
            Speed = 7f;
            transform.position += Speed * Time.deltaTime * -direction.normalized;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameStateManager.GameState = GameStateManager.State.Ended;
        }
    }

    public void Reset()
    {
        transform.position = startingPosition;
        Speed = initialSpeed;
    }
}
