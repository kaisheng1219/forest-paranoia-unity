using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footStepClip;
    [SerializeField] private GameObject inGameInstText;
    [SerializeField] private GameObject indicator, parent;

    public float TimeSinceLastDetectedTap;

    private float midX, touchPositionX, midOffset, maxMidX, minMidX;
    private Touch touch;
    private Direction playerDirection;

    private InstructionControl instructionControl;
    private WeatherControl weatherControl;
    private HeartbeatControl heartbeatControl;
    private SpeechControl speechControl;
    private GameObject boss;
    private GameObject speech;
    private Vector3 startingPosition;

    private PlayerAction controls;
    private int missedTaps;

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Tap.performed += Tapped;
        controls.Player.Hold.performed += Hold;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Tap.performed -= Tapped;
        controls.Player.Hold.performed -= Hold;
    }

    private enum Direction
    {
        TopLeft, TopRight, Up
    }

    private void Awake()
    {
        controls = new();
    }

    void Start()
    {
        weatherControl = GameObject.Find("Weather").GetComponent<WeatherControl>();
        heartbeatControl = GameObject.Find("Heartbeat").GetComponent<HeartbeatControl>();
        boss = GameObject.Find("Boss");
        speech = GameObject.Find("Speech");
        speechControl = speech.GetComponent<SpeechControl>();
        startingPosition = transform.position;

        midX = Screen.width / 2;
        midOffset = 75f;
        maxMidX = midX + midOffset;
        minMidX = midX - midOffset;
        weatherControl.StartWeather();
        speechControl.StartSpeaking();
    }
    //if (GameStateManager.GameState == GameStateManager.State.Initialized)
    //    {
            //instructionControl.StopFading();
            //weatherControl.StartWeather();
            //speechControl.StartSpeaking();
            //GameStateManager.GameState = GameStateManager.State.Started;
        //}
        //else if (GameStateManager.GameState == GameStateManager.State.Started)
        //{
        //    if (!speech.GetComponent<AudioSource>().isPlaying && !boss.GetComponent<AudioSource>().isPlaying)
        //        GameStateManager.GameState = GameStateManager.State.CanStartTapping;
        //}
    void Tapped(InputAction.CallbackContext context)
    {
        if (GameStateManager.GameState == GameStateManager.State.CanStartTapping || GameStateManager.GameState == GameStateManager.State.Invisible)
        {
            if (heartbeatControl.CanAcceptTap)
            {
                boss.GetComponent<BossControl>().Speed -= 1f;
                GameObject indi = Instantiate(indicator, controls.Player.Pos.ReadValue<Vector2>(), Quaternion.identity, parent.transform);
                indi.GetComponent<Image>().color = Color.green;
            }
            else
            {
                GameObject indi = Instantiate(indicator, controls.Player.Pos.ReadValue<Vector2>(), Quaternion.identity, parent.transform);
                indi.GetComponent<Image>().color = Color.red;
                boss.GetComponent<BossControl>().Speed += 1.5f;
                
            }

            touchPositionX = controls.Player.Pos.ReadValue<Vector2>().x;
            if (touchPositionX > maxMidX)
                playerDirection = Direction.TopRight;
            else if (touchPositionX < minMidX)
                playerDirection = Direction.TopLeft;
            else
                playerDirection = Direction.Up;

            HandleMovement();
        }
    }

    void Hold(InputAction.CallbackContext context)
    {
        //print("hold completed");
    }

    void Update()
    {
        //if (GameStateManager.GameState == GameStateManager.State.CanStartTapping)
        //    timer += Time.deltaTime;

        //int minutes = Mathf.FloorToInt(timer / 60f);
        //int seconds = Mathf.FloorToInt(timer - minutes * 60);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void HandleMovement()
    {
        //audioSource.pitch = Random.Range(0.9f, 1.0f);
        //audioSource.PlayOneShot(footStepClip); 

        if (playerDirection == Direction.TopLeft)
            transform.Translate(new Vector3(-1f, 0, 1f) * 3, Space.Self);
        else if (playerDirection == Direction.TopRight)
            transform.Translate(new Vector3(1f, 0, 1f) * 3, Space.Self);
        else
            transform.Translate(new Vector3(0, 0, 1f) * 3, Space.Self);
    }

    public void Reset()
    {
        weatherControl.StartWeather();
        transform.position = startingPosition;
    }
}
