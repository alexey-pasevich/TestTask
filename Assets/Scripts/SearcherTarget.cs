using UnityEngine;

namespace TestTaskProj
{
    public class SearcherTarget : MonoBehaviour
    {
        [SerializeField] private float m_radius = 5f; // ������ ������
        [SerializeField] private LayerMask m_layerMask; // ����� ���� ��� ������ �����
        private Collider[] m_result = new Collider[4]; // ������ ��� �������� ��������� ��������

        public Transform FindTarget()
        {
            // ����� �������� � ������� � ������ ����
            int count = Physics.OverlapSphereNonAlloc(
                transform.position,
                m_radius,
                m_result,
                m_layerMask,
                QueryTriggerInteraction.Ignore
            );

            Debug.Log($"Found {count} objects within radius."); // ��� �������

            if (count > 0)
            {
                // ������ �� ���� ��������� ��������
                for (int i = 0; i < count; i++)
                {
                    var targetCollider = m_result[i];
                    if (targetCollider != null)
                    {
                        Debug.Log($"Found object: {targetCollider.name}"); // ��� �������

                        // ��������, �������� �� ��������� ������ ������� (�� ����)
                        if (targetCollider.CompareTag("Player")) // ���������, ��� � ������ ����� ��� "Player"
                        {
                            return targetCollider.transform;
                        }
                    }
                }
            }

            return null; // ���� ������ �� �������
        }

        private void OnDrawGizmosSelected()
        {
            // ������������ ������� ������ � ���������
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }
    }
}
