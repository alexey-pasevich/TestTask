using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace TestTaskProj
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset m_inputActionAsset;
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private Transform m_cameraTarget;       // ���� ��� ������
        [SerializeField] private Transform m_cameraTransform;    // ��������� ����� ������

        [Header("Movement Settings")]
        [SerializeField] private float m_rotationSmoothTime = 0.12f;
        [SerializeField] private float m_speedChangeRate = 10f;
        [SerializeField] private float m_moveSpeed = 5f;
        [SerializeField] private float m_sprintSpeed = 10f;

        [Header("Look Settings")]
        [SerializeField] private float m_speedRotation = 200f;
        [SerializeField] private float m_topClamp = 70f;        // ����������� ������ �� ��������� �����
        [SerializeField] private float m_bottomClamp = -30f;    // ����������� ������ �� ��������� ����

        private float m_cameraPitch = 0f;                       // ��� �������� �������� ������ �� ���������
        private float m_targetRotation;                         // ������� ������� ������
        private Vector3 playerVelocity;

        // Input
        private InputActionMap m_playerMap;
        private InputAction m_moveAction;
        private InputAction m_lookAction;
        private InputAction m_jumpAction;

        private void Awake()
        {
            // ��������� Input System
            m_playerMap = m_inputActionAsset.FindActionMap("Player");
            m_moveAction = m_playerMap.FindAction("Move");
            m_lookAction = m_playerMap.FindAction("Look");
            //m_jumpAction = m_playerMap.FindAction("Jump");
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            m_playerMap.Enable();
        }

        private void OnDisable()
        {
            m_playerMap.Disable();
        }

        private void Update()
        {
            Vector2 move = m_moveAction.ReadValue<Vector2>();
            Move(move, false);

            //if (m_jumpAction.triggered && m_characterController.isGrounded)
            //{
            //    Jump();
            //}

            ApplyGravity();
        }

        private void LateUpdate()
        {
            Vector2 look = m_lookAction.ReadValue<Vector2>();
            CameraRotation(look);
        }

        private void Move(Vector2 move, bool isSprint)
        {
            float targetSpeed = move.magnitude > 0 ? (isSprint ? m_sprintSpeed : m_moveSpeed) : 0f;

            Vector3 moveDirection = new Vector3(move.x, 0f, move.y).normalized;

            if (move.magnitude > 0.1f)
            {
                // ������������ ������ � ����������� ��������
                m_targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + m_cameraTransform.eulerAngles.y;
                float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_targetRotation, ref m_rotationSmoothTime, m_rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothRotation, 0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0f, m_targetRotation, 0f) * Vector3.forward;
            Vector3 horizontalMovement = targetDirection * (targetSpeed * Time.deltaTime);

            m_characterController.Move(horizontalMovement + playerVelocity * Time.deltaTime);
        }

        private void Jump()
        {
            playerVelocity.y = Mathf.Sqrt(2f * -Physics.gravity.y * m_jumpAction.ReadValue<float>());
        }

        private void ApplyGravity()
        {
            if (m_characterController.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        private void CameraRotation(Vector2 look)
        {
            if (look.sqrMagnitude >= 0.01f)
            {
                float deltaTimeMultiplier = m_speedRotation * Time.deltaTime;

                // ��������� ���� �������� ������ �����-����
                m_cameraPitch -= look.y * deltaTimeMultiplier;
                m_cameraPitch = Mathf.Clamp(m_cameraPitch, m_bottomClamp, m_topClamp);

                // ������� ������ �� ����������� (�����-������)
                float playerYaw = transform.eulerAngles.y + look.x * deltaTimeMultiplier;

                // ��������� ����������� ���� � ������ � ������
                m_cameraTarget.rotation = Quaternion.Euler(m_cameraPitch, playerYaw, 0f);
                transform.rotation = Quaternion.Euler(0f, playerYaw, 0f);
            }
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}