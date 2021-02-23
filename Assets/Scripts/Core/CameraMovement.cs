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
