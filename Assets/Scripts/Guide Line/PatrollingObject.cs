using TMPro;
using UnityEngine;

public class PatrollingObject : MonoBehaviour
{
    [SerializeField] private AudioSource guideAudio;
    [SerializeField] private GameObject player;
    [SerializeField] private GuideControl guideControl;
    [SerializeField] private TMP_Text instructText;

    private bool canPlay, canMove;
    private Vector3 targetPosition;

    void Start()
    {
        canPlay = false;
        canMove = false;
        targetPosition = transform.position + new Vector3(-0.11f, 0, 0);
    }

    public void PlayTestAudio()
    {
        GetComponent<AudioSource>().Play();
        //canPlay = true;
        canMove = true;
    }

    void Update()
    {
        if (canPlay)
        {
            GetComponent<AudioSource>().Play();
            canPlay = false;
        }

        if (canMove)
        {
            transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                canMove = false;
                GetComponent<AudioSource>().Stop();
                guideControl.PlayNextClip();
                instructText.text = "Tap to proceed";
            }
        }        
    }
}
