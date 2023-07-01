using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialInput : MonoBehaviour
{
    public TutorialHeartbeatControl heartbeatControl;
    private PlayerAction controls;
    private int successTaps, failTaps;

    private void Awake()
    {
        controls = new();
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Tap.performed += Tapped;
        controls.Player.Hold.performed += Hold;
    }
    private void OnDisable()
    {
        controls.Player.Tap.performed -= Tapped;
        controls.Disable();
    }

    private void Update()
    {
        if (failTaps > 2)
        {
            TutorialController.Instance.Phase = TutorialController.TutorialPhase.GuidePhase;
            failTaps = 0;
            successTaps = 0;
        }
        if (successTaps > 4)
        {
            TutorialController.Instance.Phase = TutorialController.TutorialPhase.GivingInfoPhase;
            successTaps = 0;
        }
    }

    void Tapped(InputAction.CallbackContext context)
    {
        if (TutorialController.Instance.Phase == TutorialController.TutorialPhase.WaitingHeartbeatPlayerInput)
        {
            if (heartbeatControl.CanAcceptTap)
                successTaps++;
            else
                failTaps++;
        } 
        else if (TutorialController.Instance.Phase == TutorialController.TutorialPhase.WaitingPlayerInput)
        {
            SceneChanger.Instance.SwitchScene("Game");
        }
    }

    void Hold(InputAction.CallbackContext context)
    {
        if (TutorialController.Instance.Phase == TutorialController.TutorialPhase.WaitingPlayerInput)
        {
            TutorialController.Instance.Repeat();
        }
    }
}
