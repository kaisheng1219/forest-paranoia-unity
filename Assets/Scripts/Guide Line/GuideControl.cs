using UnityEngine;

public class GuideControl : MonoBehaviour
{
    [SerializeField] private PatrollingObject patrolObject;
    [SerializeField] private AudioSource objectAudio;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private GameObject repeatButton;
    [SerializeField] private GameObject guideText;

    public enum GuidePhase { WearHeadphone, TestHeadphone };
    public static GuidePhase Phase;
    private AudioClip nextClip;
    private int clipIndex;
    private bool playedPatrol;

    private void Start()
    {
        Phase = GuidePhase.WearHeadphone;
    }

    private void Update()
    {
        if (GetComponent<AudioSource>().isPlaying || objectAudio.isPlaying)
        {
            guideText.SetActive(false);
            repeatButton.SetActive(false);
        } else
        {
            guideText.SetActive(true);
            repeatButton.SetActive(true);
        }

        if (!GetComponent<AudioSource>().isPlaying && Phase == GuidePhase.TestHeadphone && !playedPatrol) 
        {
            playedPatrol = true;
            patrolObject.PlayTestAudio();
        }
    }

    public void PlayNextClip()
    {
        clipIndex++;
        nextClip = clips[clipIndex];
        GetComponent<AudioSource>().clip = nextClip;
        GetComponent<AudioSource>().Play();
    }
}
