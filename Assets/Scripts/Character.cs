using UnityEngine;
using UnityEngine.InputSystem;

namespace TestTaskProj
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private Transform m_cameraTarget;       // Цель для камеры

        [SerializeField] private float m_moveSpeed = 5f;
        [SerializeField] private float m_sprintSpeed = 10f;

        [Header("Movement Settings")]
        [SerializeField] private float m_rotationSmoothTime = 0.12f;
        [SerializeField] private float m_speedChangeRate = 10f;

        private float m_targetRotation;                         // Целевая ротация игрока
        private Vector3 playerVelocity;


        public void Move(Vector2 move, bool isSprint, float cameraY)
        {
            float targetSpeed = move.magnitude > 0 ? (isSprint ? m_sprintSpeed : m_moveSpeed) : 0f;

            Vector3 moveDirection = new Vector3(move.x, 0f, move.y).normalized;

            if (move.magnitude > 0.1f)
            {
                // Поворачиваем игрока в направлении движения
                m_targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraY;
                float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_targetRotation, ref m_rotationSmoothTime, m_rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothRotation, 0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0f, m_targetRotation, 0f) * Vector3.forward;
            Vector3 horizontalMovement = targetDirection * (targetSpeed * Time.deltaTime);

            m_characterController.Move(horizontalMovement + playerVelocity * Time.deltaTime);
        }

        public void Look(Quaternion rotation)
        {
            m_cameraTarget.rotation = rotation;
        }
    }
}
