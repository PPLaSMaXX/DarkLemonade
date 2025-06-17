using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the inspector

    public float spawnInterval; // Time in seconds between spawns
    private float spawnCounter;

    public Transform minSpawn, maxSpawn;

    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnCounter = spawnInterval; // Initialize the spawn counter
        target = PlayerController.instance.transform; // Get the player's transform to use as a reference for spawning enemies
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime; // Decrease the spawn counter by the time since last frame
        if (spawnCounter <= 0f) // Check if it's time to spawn a new enemy
        {
            spawnCounter = spawnInterval; // Reset the spawn counter
            Instantiate(enemyPrefab, SelectSpawnPoint(), transform.rotation);
        }

        transform.position = target.position; // Keep the spawner at the player's position
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnpoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f; // Randomly choose to spawn on vertical edge or horizontal edge
        if (spawnVerticalEdge)
        {
            spawnpoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y); // Randomly select a vertical position within the bounds

            if (Random.Range(0f, 1f) > .5f) // Randomly choose left or right edge
            {
                spawnpoint.x = maxSpawn.position.x; // Right edge
            }
            else
            {
                spawnpoint.x = minSpawn.position.x; // Left edge
            }
        }
        else
        {
            spawnpoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x); // Randomly select a horizontal position within the bounds

            if (Random.Range(0f, 1f) > .5f) // Randomly choose Top or Bottom edge
            {
                spawnpoint.y = maxSpawn.position.y; // Top edge
            }
            else
            {
                spawnpoint.y = minSpawn.position.y; // Bottom edge
            }
        }
        return spawnpoint; // Return the selected spawn point
    }
}
