using UnityEngine;

namespace Boxophobic.Utils
{
    public class CamController : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float accelerationMultiplier = 2f;
        public float sensitivity = 2f;

        private float _yaw = 0f;
        private float _pitch = 0f;

        void Start()
        {
            // Store the initial rotation of the camera
            _yaw = transform.eulerAngles.y;
            _pitch = transform.eulerAngles.x;
        }

        void Update()
        {
            float currentSpeed = movementSpeed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed *= accelerationMultiplier;
            }

            float horizontalMovement = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
            float verticalMovement = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

            transform.Translate(horizontalMovement, 0, verticalMovement);

            _yaw += sensitivity * Input.GetAxis("Mouse X");
            _pitch -= sensitivity * Input.GetAxis("Mouse Y");
            _pitch = Mathf.Clamp(_pitch, -90f, 90f);

            transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        }
    }
}