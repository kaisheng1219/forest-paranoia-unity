using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;

    public bool canMove;
    public Vector3 PivotPosition;
    private Vector3 targetPosition;
    private GameObject player;
    private AudioClip clip;
    private Vector3[] directions = { new Vector3(-1, 0, 1), new Vector3(0, 0, 1), new Vector3(1, 0, 1) };

    void Start()
    {
        player = GameObject.Find("Player");
        clip = clips[Random.Range(0, clips.Length)];
        targetPosition = player.transform.position + directions[Random.Range(0, directions.Length)] * 3f;
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (GameStateManager.GameDifficulty == GameStateManager.Difficulty.Hard)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                transform.RotateAround(PivotPosition, new Vector3(0, 1, 0), 20f * Time.deltaTime);
        }

        if (player.transform.position.z > transform.position.z + 3)
        {
            Destroy(gameObject, 3f);
        } else
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameStateManager.GameState = GameStateManager.State.Ended;
        }
    }
}
