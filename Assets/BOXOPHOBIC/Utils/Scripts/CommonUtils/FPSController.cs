﻿//Stripped down version from https://sharpcoderblog.com/blog/unity-3d-fps-controller

#if ENABLE_LEGACY_INPUT_MANAGER

using UnityEngine;

namespace Boxophobic.Utils
{
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public class FPSController : MonoBehaviour
    {
        public float walkingSpeed = 2.0f;
        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;

        [Space(10)]
        public GameObject playerCamera;

        UnityEngine.CharacterController _characterController;
        float _rotationX = 0;

        void Start()
        {
            _characterController = GetComponent<UnityEngine.CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            var runSpeed = 1.0f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                runSpeed = 3.0f;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            Vector3 moveDirection = (forward * walkingSpeed * runSpeed * Input.GetAxis("Vertical")) + (right * walkingSpeed * runSpeed * Input.GetAxis("Horizontal"));

            if (_characterController.isGrounded == false)
            {
                moveDirection += Physics.gravity;
            }

            _characterController.Move(moveDirection * Time.deltaTime);

            _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }
    }
}
#endif