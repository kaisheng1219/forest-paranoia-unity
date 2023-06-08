using TMPro;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footStepClip;
    [SerializeField] private GameObject inGameInstText;

    private float midX, touchPositionX, midOffset, maxMidX, minMidX;
    private Touch touch;
    private Direction playerDirection;

    private InstructionControl instructionControl;
    private WeatherControl weatherControl;
    private SpeechControl speechControl;
    private GameObject boss;
    private GameObject tutorial;
    private GameObject speech;

    private enum Direction
    {
        TopLeft, TopRight, Up
    }

    void Start()
    {
        instructionControl = GameObject.Find("Instruction").GetComponent<InstructionControl>();
        weatherControl = GameObject.Find("Weather").GetComponent<WeatherControl>();
        boss = GameObject.Find("Boss");
        tutorial = GameObject.Find("Tutorial");
        speech = GameObject.Find("Speech");
        speechControl = speech.GetComponent<SpeechControl>();

        midX = Screen.width / 2;
        midOffset = 75f;
        maxMidX = midX + midOffset;
        minMidX = midX - midOffset;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (GameStateManager.GameState == GameStateManager.State.Initialized && !tutorial.GetComponent<AudioSource>().isPlaying)
                {
                    instructionControl.StopFading();
                    weatherControl.StartWeather();
                    speechControl.StartSpeaking();
                    GameStateManager.GameState = GameStateManager.State.Started;
                }
                else if (GameStateManager.GameState == GameStateManager.State.Started)
                {
                    if (!speech.GetComponent<AudioSource>().isPlaying && !boss.GetComponent<AudioSource>().isPlaying)
                    {
                        touchPositionX = touch.position.x;

                        if (touchPositionX > maxMidX)
                            playerDirection = Direction.TopRight;
                        else if (touchPositionX < minMidX)
                            playerDirection = Direction.TopLeft;
                        else
                            playerDirection = Direction.Up;

                        HandleMovement();
                    }
                }
            }
        }
    }

    void HandleMovement()
    {
        audioSource.pitch = Random.Range(0.9f, 1.0f);
        audioSource.PlayOneShot(footStepClip); 

        if (playerDirection == Direction.TopLeft)
            transform.Translate(new Vector3(-1f, 0, 1f), Space.Self);
        else if (playerDirection == Direction.TopRight)
            transform.Translate(new Vector3(1f, 0, 1f), Space.Self);
        else
            transform.Translate(new Vector3(0, 0, 1f), Space.Self);
    }
}
