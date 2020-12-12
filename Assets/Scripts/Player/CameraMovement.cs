using System;
using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _movingDirection;
        [SerializeField] private bool _isMoving;
        [SerializeField] private float _speed = 1f;
        

        private void Update()
        {
            if (_isMoving)
                MoveCamera();
        }

        private void MoveCamera()
        {
            transform.position = Vector3.MoveTowards(transform.position, _movingDirection.position, _speed * Time.deltaTime);
        }
    }
}
