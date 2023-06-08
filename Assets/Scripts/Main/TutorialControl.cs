using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
    [SerializeField] private GameObject skipButton;

    public static bool TutorialPlayed;

    private void Start()
    {
        TutorialPlayed = true;
    }

    public void PlayTutorial()
    {
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            skipButton.SetActive(false);
    }

    public void SkipTutorial()
    {
        GetComponent<AudioSource>().Stop();
        skipButton.SetActive(false);
    }
}
