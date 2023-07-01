using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab, cabinPrefab, escapePrefab;
    [SerializeField] private GameObject parentObject;
    [SerializeField] private GameObject player;
    
    private Vector3[] allDirections = {
        new Vector3(-1, 0, 1), new Vector3(0, 0, 1), new Vector3(1, 0, 1),
        new Vector3(-1, 0, 0), new Vector3(1, 0, 0),
        new Vector3(-1, 0, -1), new Vector3(0, 0, -1), new Vector3(1, 0, -1)
    };

    private Vector3 spawnLocation;
    private GameObject newEnemy;
    private float spawnDelay = 5f;
    private int cabinCount;

    private void Start()
    {
        InvokeRepeating("SpawnCabin", 10, 30);
        Invoke("SpawnEscape", Random.Range(120f, 280f));
    }

    void Update()
    {
        if (newEnemy == null)
        {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0 && player.transform.localPosition.z > 5)
            {
                SpawnEnemy();
                spawnDelay = 5f;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (GameStateManager.GameState == GameStateManager.State.CanStartTapping)
        {
            if (GameStateManager.GameDifficulty == GameStateManager.Difficulty.Easy)
                spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, 3)] * 30f;
            else if (GameStateManager.GameDifficulty == GameStateManager.Difficulty.Medium)
                spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, 6)] * 30f;
            else if (GameStateManager.GameDifficulty == GameStateManager.Difficulty.Hard)
                spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, allDirections.Length)] * 30f;

            newEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity, parentObject.transform);
            newEnemy.GetComponent<Enemy>().PivotPosition = player.transform.localPosition;
        }
    }

    private void SpawnCabin()
    {
        if (GameStateManager.GameState == GameStateManager.State.CanStartTapping || GameStateManager.GameState == GameStateManager.State.Invisible)
        {
            spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, 3)] * 90f;
            if (Random.value < 0.2)
            {
                Instantiate(cabinPrefab, spawnLocation, Quaternion.identity, parentObject.transform);
                cabinCount++;
                if (cabinCount >= 5)
                    CancelInvoke("SpawnCabin");
            }
        }
    }

    private void SpawnEscape()
    {
        if (GameStateManager.GameState == GameStateManager.State.CanStartTapping || GameStateManager.GameState == GameStateManager.State.Invisible)
        {
            spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, 3)] * 90f;
            Instantiate(escapePrefab, spawnLocation, Quaternion.identity, parentObject.transform);
        }
    }
}
