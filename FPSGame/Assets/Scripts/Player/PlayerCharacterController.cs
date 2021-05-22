using System;
using Input;
using UnityEngine;

namespace Player
{
    public class PlayerCharacterController : MonoBehaviour
    {
        public static PlayerCharacterController Instance { get; private set; }

        [Header("引用")]
        private Camera _fppCamera;

        [Header("属性")]
        public float gravityDownForce = 20f;
        public float maxSpeedOnGround = 8f;
        public float moveSharpnessOnGround = 15f;
        public float cameraHeightRatio = 0.9f;
        private float _characterHeight = 1.8f;
        
        [Header("组件")] 
        private CharacterController _characterController;
        private PlayerInputHandler _inputHandler;

        public Vector3 CharacterVelocity { get; set; }
        private float _cameraAngle;


        #region Unity Event Functions

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _characterController = GetComponent<CharacterController>();
            _inputHandler = GetComponent<PlayerInputHandler>();
            _fppCamera = GameObject.FindWithTag("FPP Camera").GetComponent<Camera>();
        }

        private void Start()
        {
            _characterController.enableOverlapRecovery = true;
            UpdatePlayerHeight();
        }

        private void Update()
        {
            HandleRotate();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        #endregion

        
        /// <summary>
        /// 处理人物移动
        /// </summary>
        private void HandleMovement()
        {
            Vector3 moveInput = _inputHandler.GetMoveInput();
            // calculate character velocity
            // character is on ground
            if (_characterController.isGrounded)
            {
                Vector3 velocity = transform.TransformVector(moveInput * maxSpeedOnGround);

                CharacterVelocity = Vector3.Lerp(CharacterVelocity, velocity, moveSharpnessOnGround * Time.deltaTime);
            }
            // character is not on ground
            else
            {
                CharacterVelocity += Vector3.down * (gravityDownForce * Time.deltaTime);
            }
            
            // apply velocity
            _characterController.Move(CharacterVelocity * Time.deltaTime);
        }

        /// <summary>
        /// 处理人物转向和视角
        /// </summary>
        private void HandleRotate()
        {
            var mouseInput = _inputHandler.GetMouseInput();
            
            // character rotate
            transform.Rotate(new Vector3(0,mouseInput.x * Time.deltaTime,0));
            
            // fpp camera rotate
            _cameraAngle += mouseInput.y * Time.deltaTime;
            _cameraAngle = Mathf.Clamp(_cameraAngle, -89f, 89f);
            _fppCamera.transform.localEulerAngles = new Vector3(_cameraAngle,0,0);
        }

        // 更新角色高度
        private void UpdatePlayerHeight()
        {
            // 设置角色控制器的高度和重心
            _characterController.height = _characterHeight;
            _characterController.center = new Vector3(0,0.5f * _characterHeight,0);
            
            // 设置摄像机高度和角度
            _fppCamera.transform.localPosition = new Vector3(0, cameraHeightRatio * _characterController.height, 0);
        }
    }
}