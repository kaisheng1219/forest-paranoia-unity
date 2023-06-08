using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
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
        if (GameStateManager.GameState == GameStateManager.State.Started)
        {
            if (GameStateManager.GameDifficulty == GameStateManager.Difficulty.Easy)
                spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, 3)] * 3f;
            else if (GameStateManager.GameDifficulty == GameStateManager.Difficulty.Medium)
                spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, 6)] * 3f;
            else if (GameStateManager.GameDifficulty == GameStateManager.Difficulty.Hard)
                spawnLocation = player.transform.localPosition + allDirections[Random.Range(0, allDirections.Length)] * 3f;

            newEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity, parentObject.transform);
            newEnemy.GetComponent<Enemy>().PivotPosition = player.transform.localPosition;
        }
    }
}
