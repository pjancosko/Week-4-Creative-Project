using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign a Capsule prefab in Inspector
    public int numberOfEnemies = 5; // Number of enemies to spawn
    public float spawnRadius = 10f; // Radius for random spawning
    public float enemyHeight = 1f; // Fixed height for enemies to spawn
    public float moveSpeed = 2f; // Speed of enemy movement
    public float directionChangeInterval = 3f; // Time before changing direction

    private GameObject[] enemies;
    private Vector3[] directions;
    private float[] directionChangeTimers;

    void Start()
    {
        enemies = new GameObject[numberOfEnemies];
        directions = new Vector3[numberOfEnemies];
        directionChangeTimers = new float[numberOfEnemies];

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemies[i] = enemy;

            // Initialize movement direction
            directions[i] = GetRandomDirection();
            directionChangeTimers[i] = directionChangeInterval;

            // Ensure enemy has Rigidbody and CapsuleCollider
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb == null) rb = enemy.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            CapsuleCollider col = enemy.GetComponent<CapsuleCollider>();
            if (col == null) enemy.AddComponent<CapsuleCollider>();
        }
    }

    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                directionChangeTimers[i] -= Time.deltaTime;
                if (directionChangeTimers[i] <= 0)
                {
                    directions[i] = GetRandomDirection();
                    directionChangeTimers[i] = directionChangeInterval;
                }

                Rigidbody rb = enemies[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 move = directions[i] * moveSpeed * Time.deltaTime;
                    move.y = rb.linearVelocity.y; // Preserve Y velocity
                    rb.linearVelocity = move;
                }
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0,
            Random.Range(-spawnRadius, spawnRadius)
        );

        randomPosition.y = enemyHeight; // Ensure all enemies spawn at same height
        return randomPosition;
    }

    Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
