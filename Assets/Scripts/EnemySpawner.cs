using UnityEngine;

namespace TestTaskProj
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnPoints; 
        [SerializeField] private float spawnInterval = 2f; 
        [SerializeField] private int maxEnemies = 10; 

        private int currentEnemyCount = 0; 

        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }

        private void SpawnEnemy()
        {
            if (currentEnemyCount >= maxEnemies) return;

            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            currentEnemyCount++;
        }

        public void EnemyDestroyed()
        {
            if (currentEnemyCount > 0) currentEnemyCount--;
        }
    }
}