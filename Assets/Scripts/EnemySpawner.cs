using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the Inspector
    public int numberOfEnemies = 5; // Number of enemies to spawn
    public float spawnRadius = 10f; // Radius within which to spawn enemies
    public float moveSpeed = 2f; // Speed at which enemies move

    private GameObject[] enemies;

    void Start()
    {
        // Spawn enemies at random positions within the spawn radius
        enemies = new GameObject[numberOfEnemies];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 0; // Ensure the enemy is on the plane
            enemies[i] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        // Move each enemy in a random direction
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                enemy.transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
            }
        }
    }
}