using UnityEngine;

namespace TestTaskProj
{
    public class SearcherTarget : MonoBehaviour
    {
        [SerializeField] private float m_radius = 5f; // Радиус поиска
        [SerializeField] private LayerMask m_layerMask; // Маска слоя для поиска целей
        private Collider[] m_result = new Collider[4]; // Массив для хранения найденных объектов

        public Transform FindTarget()
        {
            // Поиск объектов в радиусе с учетом слоя
            int count = Physics.OverlapSphereNonAlloc(
                transform.position,
                m_radius,
                m_result,
                m_layerMask,
                QueryTriggerInteraction.Ignore
            );

            Debug.Log($"Found {count} objects within radius."); // Для отладки

            if (count > 0)
            {
                // Проход по всем найденным объектам
                for (int i = 0; i < count; i++)
                {
                    var targetCollider = m_result[i];
                    if (targetCollider != null)
                    {
                        Debug.Log($"Found object: {targetCollider.name}"); // Для отладки

                        // Проверка, является ли найденный объект игроком (по тегу)
                        if (targetCollider.CompareTag("Player")) // Убедитесь, что у игрока стоит тег "Player"
                        {
                            return targetCollider.transform;
                        }
                    }
                }
            }

            return null; // Если ничего не найдено
        }

        private void OnDrawGizmosSelected()
        {
            // Визуализация радиуса поиска в редакторе
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }
    }
}
