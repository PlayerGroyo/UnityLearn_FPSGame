using UnityEngine;

namespace Input
{
    public class PlayerInputHandler : MonoBehaviour
    {

        public static PlayerInputHandler Instance { get; private set; }

        public float mouseXSensitivity = 10f;
        public float mouseYSensitivity = 10f;
        public bool mouseXInverse;
        public bool mouseYInverse;

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
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        /// <summary>
        /// 获取玩家的移动输入
        /// </summary>
        /// <returns>模不大于1的方向向量</returns>
        public Vector3 GetMoveInput()
        {
            Vector3 move = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0,
                UnityEngine.Input.GetAxis("Vertical"));
            move = Vector3.ClampMagnitude(move, 1f);
            return move;
        }

        /// <summary>
        /// 获取鼠标输入
        /// </summary>
        /// <returns>鼠标输入向量</returns>
        public Vector2 GetMouseInput()
        {
            float mouseX = mouseXInverse ? UnityEngine.Input.GetAxis("Mouse X"): -UnityEngine.Input.GetAxis("Mouse X");
            float mouseY = mouseYInverse ? UnityEngine.Input.GetAxis("Mouse Y"): -UnityEngine.Input.GetAxis("Mouse Y");
            
            return new Vector2(mouseX*mouseXSensitivity, mouseY*mouseYSensitivity);
        }
    }
}
