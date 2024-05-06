using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollectibleManager : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public Transform[] spawnPoints;

    private Dictionary<Transform, bool> spawnPointOccupied = new Dictionary<Transform, bool>();
    private HashSet<string> spawnedCollectibleTypes = new HashSet<string>();

    void Start()
    {
        // Initialize spawn points as unoccupied
        foreach (Transform spawnPoint in spawnPoints)
        {
            spawnPointOccupied.Add(spawnPoint, false);
        }

        // Spawn collectibles instantly after the game starts
        SpawnCollectibles();
    }

    void SpawnCollectibles()
    {
        // Iterate over each spawn point
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Get an unoccupied spawn point
            if (!spawnPointOccupied[spawnPoint])
            {
                // Get a collectible prefab that hasn't been spawned yet
                GameObject collectiblePrefab = GetUnusedCollectible();
                if (collectiblePrefab != null)
                {
                    // Spawn collectible at the spawn point
                    Instantiate(collectiblePrefab, spawnPoint.position, Quaternion.identity);

                    // Mark spawn point as occupied
                    spawnPointOccupied[spawnPoint] = true;
                    // Mark collectible type as spawned
                    spawnedCollectibleTypes.Add(collectiblePrefab.name);
                }
            }
        }
    }

    GameObject GetUnusedCollectible()
    {
        // Check each collectible type if it has been spawned
        foreach (GameObject collectiblePrefab in collectiblePrefabs)
        {
            if (!spawnedCollectibleTypes.Contains(collectiblePrefab.name))
            {
                return collectiblePrefab;
            }
        }

        // If all collectible types have been spawned, return null
        return null;
    }
}
