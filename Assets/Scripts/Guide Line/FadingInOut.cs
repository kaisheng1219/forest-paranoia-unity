using UnityEngine;

public class FadingInOut : MonoBehaviour
{
    private bool fadeIn, fadeOut;

    void Start()
    {
        fadeOut = true;
        fadeIn = false;
    }

    void Update()
    {
        if (fadeOut)
        {
            if (GetComponent<CanvasGroup>().alpha >= 0)
            {
                GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
                if (GetComponent<CanvasGroup>().alpha <= 0)
                {
                    fadeOut = false;
                    fadeIn = true;
                }
            }
        }

        if (fadeIn)
        {
            if (GetComponent<CanvasGroup>().alpha < 1)
            {
                GetComponent<CanvasGroup>().alpha += Time.deltaTime;
                if (GetComponent<CanvasGroup>().alpha >= 1)
                {
                    fadeIn = false;
                    fadeOut = true;
                }
            }
        }
    }
}
