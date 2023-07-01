using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private AudioSource[] allAudioSources;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (var source in allAudioSources)
        {
            source.Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (var source in allAudioSources)
        {
            source.UnPause();
        }
        pausePanel.SetActive(false);
    }
}
