using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinControl : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private BossControl bossControl;
    [SerializeField] private PlayerInput player;

    private AudioSource[] allAudioSources;
    private bool gameWon;
    
    void Start()
    {
        winPanel.SetActive(false);
    }

    void Update()
    {
        if (GameStateManager.GameState == GameStateManager.State.Won && !gameWon)
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var source in allAudioSources)
            {
                source.Stop();
            }
            GetComponent<AudioSource>().Play();
            winPanel.SetActive(true);
            string minutes = TimeSpan.FromSeconds(GameStateManager.GameTime).ToString("mm");
            string seconds = TimeSpan.FromSeconds(GameStateManager.GameTime).ToString("ss");

            timeText.text = minutes + "m " + seconds + "s";
            gameWon = true;
        }
    }
}
