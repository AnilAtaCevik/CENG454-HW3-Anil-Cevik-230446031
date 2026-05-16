using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Core targetCore;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;

    private bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            if (enemyScript != null && targetCore != null)
            {
                enemyScript.SetTarget(targetCore);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}