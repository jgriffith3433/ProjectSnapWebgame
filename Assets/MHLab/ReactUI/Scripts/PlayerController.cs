using System;
using UnityEngine;

namespace MHLab.ReactUI.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float Speed = 2f;
        public float TurnSmoothTime = 0.2f;
        public float SpeedSmoothTime = 0.1f;
        public Transform CameraTransform;
        
        private float _turnSmoothVelocity;
        private float _speedSmoothVelocity;
        private float _currentSpeed;

        protected void Update()
        {
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var inputDirection = input.normalized;

            if (inputDirection != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSmoothVelocity, TurnSmoothTime);
            }

            float targetSpeed = Speed * inputDirection.magnitude;
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _speedSmoothVelocity, SpeedSmoothTime);
            
            transform.Translate(_currentSpeed * Time.deltaTime * transform.forward, Space.World);
        }
    }
}