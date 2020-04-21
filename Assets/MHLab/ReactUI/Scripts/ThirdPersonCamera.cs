using System;
using UnityEngine;

namespace MHLab.ReactUI.Scripts
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public bool LockCursor;
        public float MouseSensitivity = 5f;

        public Transform CameraTarget;
        public float DistanceFromTarget = 3f;
        
        public Vector2 PitchThreshold = new Vector2(-5, 75);
        public float RotationSmoothTime = 0.2f;

        private Vector3 _rotationSmoothVelocity;
        private Vector3 _currentRotation;
        private float _yaw;
        private float _pitch;

        protected void Start()
        {
            if (LockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        protected void LateUpdate()
        {
            _yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
            _pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
            _pitch = Mathf.Clamp(_pitch, PitchThreshold.x, PitchThreshold.y);

            _currentRotation = Vector3.SmoothDamp(_currentRotation, new Vector3(_pitch, _yaw), ref _rotationSmoothVelocity, RotationSmoothTime);
            transform.eulerAngles = _currentRotation;

            transform.position = CameraTarget.position - transform.forward * DistanceFromTarget;
        }
    }
}