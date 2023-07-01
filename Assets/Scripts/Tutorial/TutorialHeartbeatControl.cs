using UnityEngine;
using UnityEngine.Audio;

public class TutorialHeartbeatControl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform effect;

    public bool CanStartPlaying;
    public bool CanAcceptTap;
    private int bpm = 60;
    private int timesPlayed;
    private float timeSinceLastPlayed, timeToTakeInput;
    private float[] samples = new float[512];

    void Start()
    {
        effect.gameObject.SetActive(false);
    }

    void Update()
    {
        if (CanStartPlaying)
        {
            if (timeSinceLastPlayed <= 0)
            {
                audioSource.PlayOneShot(audioSource.clip);
                timeSinceLastPlayed = bpm / 60f;
                timesPlayed++;
            }
            float effectScale = CalculateSpectrum() * 100;
            if (effectScale > 10)
            {
                effect.gameObject.SetActive(true);
                CanAcceptTap = true;
                timeToTakeInput = 0.15f;
            }
            else
            {
                effect.gameObject.SetActive(false);
                timeToTakeInput -= Time.deltaTime;
            }

            if (CanAcceptTap && timeToTakeInput < 0)
                CanAcceptTap = false;
            
            timeSinceLastPlayed -= Time.deltaTime;
        }
        
    }

    private void GetSpectrum()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private float CalculateSpectrum()
    {
        GetSpectrum();
        float total = 0f;
        foreach (var sample in samples)
        {
            total += sample;
        }
        return total;
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    public void Reset()
    {
        CanStartPlaying = true;
        timeSinceLastPlayed = 0;
    }
}
