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

            // Ensure the enemy has a Rigidbody component
            Rigidbody rb = enemies[i].GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = enemies[i].AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation to prevent tipping over
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
                Rigidbody rb = enemies[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 move = directions[i] * moveSpeed * Time.deltaTime;
                    move.y = rb.linearVelocity.y; // Preserve the current Y velocity (gravity)
                    rb.linearVelocity = move;
                }
            }
        }
    }

    Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        return randomDirection;
    }
}