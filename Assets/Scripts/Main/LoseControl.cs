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

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GameState == GameStateManager.State.Ended && !gameEnded)
        {
            GetComponent<AudioSource>().Play();
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var source in allAudioSources)
            {
                if (source.clip.name != "chew")
                    source.Stop();
            }

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
        GameStateManager.Reset();
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        GameStateManager.Reset();
        GetComponent<AudioSource>().Stop();
        losePanel.SetActive(false);
        bossControl.Reset();
        player.Reset();
        gameEnded = false;
        GameStateManager.GameState = GameStateManager.State.Started;
    }
}
