using System.Collections;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab; // The fire VFX prefab
    public float fireSpawnIntervalMin = 1f; // Minimum interval between fire spawns
    public float fireSpawnIntervalMax = 3f; // Maximum interval between fire spawns
    public float fireSpawnDistance = 2f; // Distance behind the chaser to spawn fire
    public float fireSpawnRadius = 1f; // Radius within which fire can spawn behind the chaser

    private Coroutine fireSpawnCoroutine;

    private void Start()
    {
        fireSpawnCoroutine = StartCoroutine(SpawnFireRoutine());
    }

    private IEnumerator SpawnFireRoutine()
    {
        while (true)
        {
            // Wait for a random interval between fire spawns
            float waitTime = Random.Range(fireSpawnIntervalMin, fireSpawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            // Spawn fire at a random position behind the NPC
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(firePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Get a random direction in a circle around the NPC
        Vector2 randomCircle = Random.insideUnitCircle * fireSpawnRadius;
        Vector3 randomOffset = new Vector3(randomCircle.x, 0, randomCircle.y);

        // Calculate the position behind the NPC
        Vector3 spawnPosition = transform.position - transform.forward * fireSpawnDistance + randomOffset;
        return spawnPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered by: " + other.gameObject.name); // Debug log
        if (other.CompareTag("Finish"))
        {
            Debug.Log("StopFireTrigger activated!"); // Debug log
            if (fireSpawnCoroutine != null)
            {
                StopCoroutine(fireSpawnCoroutine);
            }
            this.enabled = false; // Disable the FireSpawner script
        }
    }
}
