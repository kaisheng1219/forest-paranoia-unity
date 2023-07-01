using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance { get; private set; }
    public GameObject pausePanel;
    public TutorialHeartbeatControl heartbeatControl;
    public AudioClip heartbeatInstClip, heartbeatGuideClip, infoClip, endClip;
    public TutorialPhase Phase;

    private AudioSource audioSource;

    public enum TutorialPhase
    {
        HeartbeatPhase, GivingInfoPhase, WaitingHeartbeatPlayerInput, GuidePhase, WaitingPlayerInput, RejectInput, EndingPhase
    }

    private void Awake()
    {
        Instance = this;
        if (SceneChanger.Instance != null)
            SceneChanger.Instance.InitScene();
        audioSource = GetComponent<AudioSource>();
        Phase = TutorialPhase.HeartbeatPhase;
        pausePanel.SetActive(false);
        PlayHeartbeatInst();
    }

    void Update()
    {
        if (Phase == TutorialPhase.HeartbeatPhase && !audioSource.isPlaying)
        {
            heartbeatControl.CanStartPlaying = true;
            Phase = TutorialPhase.WaitingHeartbeatPlayerInput;
        } 

        if (Phase == TutorialPhase.GuidePhase && !audioSource.isPlaying)
        {
            heartbeatControl.CanStartPlaying = false;
            PlayHeartbeatGuide();
        }

        if (Phase == TutorialPhase.GivingInfoPhase && !audioSource.isPlaying)
        {
            heartbeatControl.CanStartPlaying = false;
            PlayInfo();
        }
    }

    public void PlayHeartbeatInst()
    {
        audioSource.PlayOneShot(heartbeatInstClip);
    }
    public void PlayHeartbeatGuide()
    {
        audioSource.PlayOneShot(heartbeatGuideClip);
        Invoke("restartHeartbeat", heartbeatGuideClip.length);
    }
    public void PlayInfo()
    {
        Phase = TutorialPhase.EndingPhase;
        audioSource.PlayOneShot(infoClip);
        Invoke("playEnding", infoClip.length);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        audioSource.Pause();
        heartbeatControl.Pause();
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        audioSource.UnPause();
        heartbeatControl.UnPause();
        pausePanel.SetActive(false);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneChanger.Instance.SwitchScene("MainMenu");
    }

    private void restartHeartbeat()
    {
        heartbeatControl.Reset();
        Phase = TutorialPhase.WaitingHeartbeatPlayerInput;
    }

    private void playEnding()
    {
        audioSource.PlayOneShot(endClip);
        Invoke("waitForFinalInput", endClip.length);
    }

    private void waitForFinalInput()
    {
        Phase = TutorialPhase.WaitingPlayerInput;
    }
    public void Repeat()
    {
        Phase = TutorialPhase.RejectInput;
        PlayInfo();
    }

    public void Skip()
    {
        if (SceneChanger.Instance != null)
            SceneChanger.Instance.SwitchScene("Game");
    }
}
