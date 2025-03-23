using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
     [System.Serializable]
    public class Wave
    {
        public int totalSpawnEnemies;
        public int numberOfRandomSpawnPoint;
        public float delayStart;
        public float spawnInterval;
        public int numberOfPowerUp;
    }

    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public List<Wave> waves;
    private Queue<Wave> waveQueue;
    private List<Transform> activeSpawnPoints;

    private void Start()
    {
        waveQueue = new Queue<Wave>(waves);
        StartCoroutine(StartWaveSequence());
    }

    IEnumerator StartWaveSequence()
    {
        while (waveQueue.Count > 0)
        {
            Wave currentWave = waveQueue.Dequeue();
            yield return new WaitForSeconds(currentWave.delayStart);

            activeSpawnPoints = GetRandomSpawnPoints(currentWave.numberOfRandomSpawnPoint);
            StartCoroutine(SpawnEnemies(currentWave));
            SpawnPowerUps(currentWave.numberOfPowerUp);

            yield return new WaitForSeconds(currentWave.totalSpawnEnemies * currentWave.spawnInterval);
        }
    }

    IEnumerator SpawnEnemies(Wave wave)
    {
        for (int i = 0; i < wave.totalSpawnEnemies; i++)
        {
            Transform spawnPoint = activeSpawnPoints[Random.Range(0, activeSpawnPoints.Count)];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    private List<Transform> GetRandomSpawnPoints(int count)
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);
        List<Transform> selectedPoints = new List<Transform>();
        
        for (int i = 0; i < count; i++)
        {
            Transform point = availablePoints[Random.Range(0, availablePoints.Count)];
            selectedPoints.Add(point);
            availablePoints.Remove(point);
        }
        return selectedPoints;
    }

    private void SpawnPowerUps(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(powerUpPrefab, randomPoint.position, Quaternion.identity);
        }
    }
}
