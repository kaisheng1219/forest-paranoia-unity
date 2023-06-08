using TMPro;
using UnityEngine;

public class InstructionControl : MonoBehaviour
{
    [SerializeField] private CanvasGroup instructionGroup;
    [SerializeField] private TMP_Text description;
    [SerializeField] private GameObject icon;

    private GameObject tutorial;
    private bool fadeIn, fadeOut;

    void Start()
    {
        tutorial = GameObject.Find("Tutorial");
        fadeOut = true;
        fadeIn = false;
    }

    void Update()
    {
        if (tutorial.GetComponent<AudioSource>().isPlaying)
        {
            description.text = "Tutorial";
            icon.SetActive(false);

        } else
        {
            description.text = "Tap to start";
            icon.SetActive(true);
        }

        if (fadeOut)
        {
            if (instructionGroup.alpha >= 0)
            {
                instructionGroup.alpha -= Time.deltaTime;
                if (instructionGroup.alpha <= 0)
                {
                    fadeOut = false;
                    fadeIn = true;
                }
            }
        }

        if (fadeIn)
        {
            if (instructionGroup.alpha < 1)
            {
                instructionGroup.alpha += Time.deltaTime;
                if (instructionGroup.alpha >= 1)
                {
                    fadeIn = false;
                    fadeOut = true;
                }
            }
        }
    }

    public void StopFading()
    {
        instructionGroup.alpha = 0;
        fadeIn = false;
        fadeOut = false;
        gameObject.SetActive(false);
    }
}
