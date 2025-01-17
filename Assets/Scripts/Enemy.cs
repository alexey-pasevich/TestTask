using UnityEngine;
using UnityEngine.AI;

namespace TestTaskProj
{
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent m_agent;

        private void Awake()
        {
            m_agent = GetComponent<NavMeshAgent>();
        }

        void Start () 
        {
            RandomPoint();
        }

        private void RandomPoint()
        {
            Vector2 target = Random.insideUnitCircle * 10f;
            m_agent.SetDestination(new Vector3(target.x, 0, target.y));
        }

        private void Update()
        {
            if (m_agent.remainingDistance <= m_agent.stoppingDistance)
            {
                Debug.Log("New point");
                RandomPoint();
            }
        }
        private void OnDestroy()
        {
            // Находим спавнер и уведомляем его об уничтожении врага
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.EnemyDestroyed();
            }
        }
    }
}
