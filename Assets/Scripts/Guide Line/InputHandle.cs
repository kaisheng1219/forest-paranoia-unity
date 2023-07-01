using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandle : MonoBehaviour
{
    [SerializeField] private GuideControl guideControl;
    [SerializeField] private AudioSource guideAudio;
    [SerializeField] private AudioSource objectAudio;

    private PlayerAction action;

    private void Awake()
    {
        action = new();
    }

    private void OnEnable()
    {
        action.Enable();
        action.Player.Tap.performed += OnTap;
        action.Player.Hold.performed += OnHold;
    }

    private void OnDisable()
    {
        action.Player.Tap.performed -= OnTap;
        action.Player.Hold.performed -= OnHold;
        action.Disable();
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        if (!guideAudio.isPlaying && !objectAudio.isPlaying)
        {
            if (GuideControl.Phase == GuideControl.GuidePhase.WearHeadphone)
            {
                GuideControl.Phase = GuideControl.GuidePhase.TestHeadphone;
                guideControl.PlayNextClip();
            }
            else if (GuideControl.Phase == GuideControl.GuidePhase.TestHeadphone)
                SceneChanger.Instance.SwitchScene("MainMenu");
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (!guideAudio.isPlaying && !objectAudio.isPlaying)
        {
            Repeat();
        }
    }


    public void Repeat()
    {
        guideAudio.Play();
    }
}
