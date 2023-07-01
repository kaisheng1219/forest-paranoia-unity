using System;
using TMPro;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private SpeechControl playerSpeech;
    [SerializeField] private BossControl boss;
    [SerializeField] private TMP_Text timerText;

    public enum State
    {
        Started, CanStartTapping, Ended, Won, Invisible
    }

    public enum Difficulty
    {
        Easy, Medium, Hard
    }

    public static State GameState { get; set; }
    public static Difficulty GameDifficulty { get; set; }

    public static float GameTime;

    private void Start()
    {
        SceneChanger.Instance.InitScene();
        Time.timeScale = 1;
        GameState = State.Started;
        GameTime = 0;
        GameDifficulty = Difficulty.Easy;
    }

    private void Update()
    {
        if (GameState == State.Ended || GameState == State.Won)
        {
            Time.timeScale = 0;
        }
        else if (GameState == State.Started)
        {
            if (!playerSpeech.GetComponent<AudioSource>().isPlaying && !boss.GetComponent<AudioSource>().isPlaying)
                GameState = State.CanStartTapping;
        }
        else if (GameState == State.CanStartTapping || GameState == State.Invisible)
        {
            GameTime += Time.deltaTime;
        }

        if (Time.time >= 60)
            GameDifficulty = Difficulty.Medium;
        else if (Time.time >= 120) 
            GameDifficulty = Difficulty.Hard;

        string mins = TimeSpan.FromSeconds(GameTime).ToString("mm");
        string secs = TimeSpan.FromSeconds(GameTime).ToString("ss");

        if (mins == "05")
        {
            GameState = State.Won;
            GameTime = 300;
            timerText.text = "05:00";
        } 
        else
            timerText.text = string.Format("{0:00}:{1:00}", mins, secs);
    }

    public static void Reset()
    {
        GameState = State.CanStartTapping;
        GameDifficulty = Difficulty.Easy;
        GameTime = 0;
        Time.timeScale = 1f;
    }
}
