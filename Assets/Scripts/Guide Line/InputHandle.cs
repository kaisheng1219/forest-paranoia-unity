using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InputHandle : MonoBehaviour
{
    [SerializeField] private GuideControl guideControl;
    [SerializeField] private AudioSource guideAudio;
    [SerializeField] private AudioSource objectAudio;

    private Touch touch;

    void Update()
    {
        if (!guideAudio.isPlaying && !objectAudio.isPlaying && Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began && touch.position.y < 0.8 * Screen.height)
            {
                if (GuideControl.Phase == GuideControl.GuidePhase.WearHeadphone)
                {
                    GuideControl.Phase = GuideControl.GuidePhase.TestHeadphone;
                    guideControl.PlayNextClip();
                } else if (GuideControl.Phase == GuideControl.GuidePhase.TestHeadphone)
                    SceneManager.LoadScene(1);
            }
        }
    }

    public void Repeat()
    {
        guideAudio.Play();
    }
}
