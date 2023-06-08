using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum State
    {
        Initialized, Started, Ended
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
        Time.timeScale = 1;
        GameState = State.Initialized;
        GameDifficulty = Difficulty.Easy;
    }

    private void Update()
    {
        if (GameState == State.Ended)
        {
            Time.timeScale = 0;
        }
        else if (GameState == State.Started)
        {
            GameTime += Time.deltaTime;
        }

        if (Time.time >= 60)
            GameDifficulty = Difficulty.Medium;
        else if (Time.time >= 120) 
            GameDifficulty = Difficulty.Hard;
    }

    public static void Reset()
    {
        GameState = State.Initialized;
        GameDifficulty = Difficulty.Easy;
        GameTime = 0;
    }
}
