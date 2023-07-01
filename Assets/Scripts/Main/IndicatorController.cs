using UnityEngine;

public class IndicatorController : MonoBehaviour
{ 
    public void OnFadeOutDone()
    {
        Destroy(gameObject);
    }
}
