using UnityEngine;

public class SpeechControl : MonoBehaviour
{
    public void StartSpeaking()
    {
        GetComponent<AudioSource>().Play();
    }
}
