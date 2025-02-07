using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the Inspector
    public int numberOfEnemies = 5; // Number of enemies to spawn
    public float spawnRadius = 10f; // Radius within which to spawn enemies
    public float moveSpeed = 2f; // Speed at which enemies move
    public float directionChangeInterval = 3f; // Time interval to change direction

    private GameObject[] enemies;
    private Vector3[] directions;
    private float[] directionChangeTimers;

    void Start()
    {
        // Spawn enemies at random positions within the spawn radius
        enemies = new GameObject[numberOfEnemies];
        directions = new Vector3[numberOfEnemies];
        directionChangeTimers = new float[numberOfEnemies];

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 0; // Ensure the enemy is on the plane
            enemies[i] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Initialize direction and timer
            directions[i] = GetRandomDirection();
            directionChangeTimers[i] = directionChangeInterval;
        }
    }

    void Update()
    {
        // Move each enemy in a random direction
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                // Update direction change timer
                directionChangeTimers[i] -= Time.deltaTime;
                if (directionChangeTimers[i] <= 0)
                {
                    directions[i] = GetRandomDirection();
                    directionChangeTimers[i] = directionChangeInterval;
                }

                // Move the enemy
                enemies[i].transform.Translate(directions[i] * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        return randomDirection;
    }
}