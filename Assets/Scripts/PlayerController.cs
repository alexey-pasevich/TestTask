using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace TestTaskProj
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character m_character;
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private InputActionAsset m_inputActionAsset;
        [SerializeField] private Transform m_cameraTransform;    // Трансформ самой камеры
        [SerializeField] private Transform m_cameraTarget;

        [Header("Look Settings")]
        [SerializeField] private float m_speedRotation = 200f;
        [SerializeField] private float m_topClamp = 70f;        // Ограничение обзора по вертикали вверх
        [SerializeField] private float m_bottomClamp = -30f;    // Ограничение обзора по вертикали вниз

        private float m_cameraPitch = 0f;                       // Для контроля вращения камеры по вертикали
        private Vector3 playerVelocity;

        // Input
        private InputActionMap m_playerMap;
        private InputAction m_moveAction;
        private InputAction m_lookAction;
        private InputAction m_jumpAction;
        private InputAction m_attackAction;


        private void Awake()
        {
            // Настройка Input System
            m_playerMap = m_inputActionAsset.FindActionMap("Player");
            m_moveAction = m_playerMap.FindAction("Move");
            m_lookAction = m_playerMap.FindAction("Look");
            m_jumpAction = m_playerMap.FindAction("Jump");
            m_attackAction = m_playerMap.FindAction("Fire");
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            m_playerMap.Enable();

            m_attackAction.performed += OnFireInput;
        }

        private void OnDisable()
        {
            m_playerMap.Disable();

            m_attackAction.performed += OnFireInput;
        }

        private void Update()
        {
            Vector2 move = m_moveAction.ReadValue<Vector2>();
            Move(move, false);

            if (m_jumpAction.triggered && m_characterController.isGrounded)
            {
                Jump();
            }

            ApplyGravity();
        }

        private void OnFireInput(InputAction.CallbackContext context) 
        {
            Debug.Log("Fire");
        }

        private void LateUpdate()
        {
            Vector2 look = m_lookAction.ReadValue<Vector2>();
            CameraRotation(look);
        }

        private void Move(Vector2 move, bool isSprint)
        {
            m_character.Move(move, isSprint, m_cameraTransform.eulerAngles.y);
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

                // Обновляем угол поворота камеры вверх-вниз
                m_cameraPitch -= look.y * deltaTimeMultiplier;
                m_cameraPitch = Mathf.Clamp(m_cameraPitch, m_bottomClamp, m_topClamp);

                // Поворот игрока по горизонтали (влево-вправо)
                float playerYaw = transform.eulerAngles.y + look.x * deltaTimeMultiplier;

                // Применяем обновленные углы к камере и игроку
                m_cameraTarget.rotation = Quaternion.Euler(m_cameraPitch, playerYaw, 0f);
                transform.rotation = Quaternion.Euler(0f, playerYaw, 0f);
            }
        }

        //private static float ClampAngle(float angle, float min, float max)
        //{
        //    if (angle < -360f) angle += 360f;
        //    if (angle > 360f) angle -= 360f;
        //    return Mathf.Clamp(angle, min, max);
        //}
    }
}