using UnityEngine;

public class RadarControl : MonoBehaviour
{
    [SerializeField] private GameObject radar;
    private bool radarEnabled;

    void Start()
    {
        radar.SetActive(false);
    }

    void Update()
    {
        if (!radarEnabled && GameStateManager.GameState == GameStateManager.State.Started) 
        {
            radar.SetActive(true);
            radarEnabled = true;
        }
    }
}
