using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseControl : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private BossControl bossControl;
    [SerializeField] private PlayerInput player;

    private AudioSource[] allAudioSources;
    private bool gameEnded;

    private void Start()
    {
        losePanel.SetActive(false);
    }

    void Update()
    {
        if (GameStateManager.GameState == GameStateManager.State.Ended && !gameEnded)
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var source in allAudioSources)
            {
                //if (source.clip.name != "chew")
                source.Stop();
            }
            GetComponent<AudioSource>().Play();
            losePanel.SetActive(true);
            string minutes = TimeSpan.FromSeconds(GameStateManager.GameTime).ToString("mm");
            string seconds = TimeSpan.FromSeconds(GameStateManager.GameTime).ToString("ss");

            timeText.text = minutes + "m " + seconds + "s";
            gameEnded = true;
        }
    }

    public void ExitToMainMenu()
    {
        GetComponent<AudioSource>().Stop();
        Time.timeScale = 1;
        GameStateManager.Reset();
        SceneChanger.Instance.SwitchScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        GetComponent<AudioSource>().Stop();
        GameStateManager.Reset();
        SceneChanger.Instance.SwitchScene("Game");
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
