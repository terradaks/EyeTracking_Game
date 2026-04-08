using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    public float spawnDelay = 1.5f;

    private List<Transform> freePoints = new List<Transform>();

    void Start()
    {
        // Initially all points are free
        freePoints.AddRange(spawnPoints);

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (freePoints.Count == 0)
            return; // no space available

        // Pick random free spawn point
        Transform point = freePoints[Random.Range(0, freePoints.Count)];

        // Pick random enemy type
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Spawn
        GameObject enemy = Instantiate(prefab, point.position, Quaternion.identity);

        // Mark as occupied
        freePoints.Remove(point);

        // Setup enemy
        EnemyController ec = enemy.GetComponent<EnemyController>();
        ec.spawnPoint = point;
        ec.spawner = this;
    }

    public void FreePoint(Transform point)
    {
        freePoints.Add(point);
    }
}