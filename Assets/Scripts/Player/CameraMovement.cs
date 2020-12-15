using System;
using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _movingDirection;
        [SerializeField] private bool _isMoving;
        [SerializeField] private float _speed = 1f;

        private Vector3 _lastSavedPosition;
        
        private void Update()
        {
            if (_isMoving)
                MoveCamera();
        }

        private void MoveCamera()
        {
            _lastSavedPosition = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _movingDirection.position, _speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, _lastSavedPosition.y, _lastSavedPosition.z);
        }
    }
}
