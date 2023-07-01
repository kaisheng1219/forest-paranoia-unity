using UnityEngine;
using UnityEngine.Audio;

[DefaultExecutionOrder(1)]
public class HeartbeatControl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private Transform effect;

    public bool CanStartPlaying;
    public bool CanAcceptTap;
    public float BPM { get { return bpm; } }

    private const int initialBpm = 60;
    private int bpm = 60;
    private float initialPitch;
    private float timeSinceLastPlayed;
    private float timeToChange;

    private float timeToTakeInput;
    float timeElapsedSinceLastPlayed = 0;


    private float[] samples = new float[512];

    private void Start()
    {
        bpm = initialBpm;
        effect.gameObject.SetActive(false);
        //initialPitch = 1f;
        //SetPitch(initialPitch);
    }

    private void Update()
    {
        if (GameStateManager.GameState == GameStateManager.State.CanStartTapping)
        {
            if (timeToChange <= 0)
            {
                GenerateRandomBpm();
            }
            if (timeSinceLastPlayed <= 0)
            {
                audioSource.PlayOneShot(audioSource.clip);
                //audioSource.Play();
                timeSinceLastPlayed = bpm / 60f;
            }
            float effectScale = CalculateSpectrum() * 100;
            if (effectScale > 10)
            {
                effect.gameObject.SetActive(true);
                CanAcceptTap = true;
                timeElapsedSinceLastPlayed = 0;
                timeToTakeInput = 0.2f;
            } else
            {
                effect.gameObject.SetActive(false);
                timeToTakeInput -= Time.deltaTime;
                timeElapsedSinceLastPlayed += Time.deltaTime;
            }

            if (CanAcceptTap && timeToTakeInput < 0)
            {
                CanAcceptTap = false;
            }
            //effect.localScale = new Vector3(effectScale, effectScale);
            timeSinceLastPlayed -= Time.deltaTime;
            timeToChange -= Time.deltaTime;
            //if (timeSinceLastPlayed >= bpm / 60)
            //{
            //    audioSource.PlayOneShot(audioSource.clip);
            //    timeSinceLastPlayed = 0;
            //} else
            //{
            //    timeSinceLastPlayed += Time.deltaTime;
            //}
        }
    }

    private void GenerateRandomBpm()
    {
        bpm = Random.Range(50, 90);
        timeToChange = Random.Range(10f, 20f);
    }

    public void SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
        //audioMixerGroup.audioMixer.SetFloat("Heartbeat_Pitch_Bend", 1f / pitch);
        //audioMixerGroup.audioMixer.SetFloat("Heartbeat_Pitch_Bend", pitch);
        print("pitch : " + pitch);
    }

    public void ResetBeating()
    {
        bpm = initialBpm;
        timeSinceLastPlayed = 0;
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
}
