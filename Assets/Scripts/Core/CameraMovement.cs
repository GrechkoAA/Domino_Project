using System;
using System.Collections;
using Ground;
using UnityEngine;

namespace Core
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _movingDirection;
        [SerializeField] private bool _isMoving;
        [SerializeField] private float _speed = 5f;

        private float _smoothness = 0.125f;
        
        private Vector3 _lastSavedPosition;

        private void LateUpdate()
        {
            if (_isMoving)
                MoveCamera();
        }

        private void MoveCamera()
        {
            _lastSavedPosition = transform.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, _movingDirection.transform.position, _smoothness * _speed * Time.deltaTime);
            transform.position = new Vector3(smoothedPosition.x, _lastSavedPosition.y, smoothedPosition.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<FinishGround>() != null && _speed > 0)
                StartCoroutine(SlowDownSpeed());
        }

        private IEnumerator SlowDownSpeed()
        {
            while (_speed > 0)
            {
                _speed -= 0.05f;
                yield return new WaitForSeconds(0.2f);
            }

            if (_speed < 0)
                _speed = 0;
        }
    }
}
