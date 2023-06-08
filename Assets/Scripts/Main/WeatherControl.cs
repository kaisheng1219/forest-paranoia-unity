using UnityEngine;

public class WeatherControl : MonoBehaviour
{
    public void StartWeather()
    {
        GetComponent<AudioSource>().Play();
    }
}
