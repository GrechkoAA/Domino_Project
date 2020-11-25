using System;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _minimalDistanceBetweenFigures = 0.2f;
        [SerializeField] private Camera _camera;
        
        private Vector3 _spawnPosition;
        private Transform _lastSpawnedFigure;

        private void Update()
        {
            TryRaycast();
        }

        private void TryRaycast()
        {
            if (Input.GetMouseButton(0))
                if (CanPlaceFigure())
                    Spawn(_spawnPosition);
        }

        private bool CanPlaceFigure()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay(), Mathf.Infinity);
            if (hits.Length == 0) return false;

            foreach (var hit in hits)
            {
                if (hit.collider.GetComponent<DominoFigure>() != null) continue;
                
                var sphereHits = Physics.SphereCastAll(hit.point, _minimalDistanceBetweenFigures, Vector3.up);
                foreach (var sphereHit in sphereHits)
                    if (sphereHit.collider.GetComponent<DominoFigure>() != null) 
                        return false;
            }

            _spawnPosition = hits[0].point;
            return true;
        }

        private void Spawn(Vector3 position)
        {
            var domino = Instantiate(_prefab, position + Vector3.up, Quaternion.identity).transform;
            
            if (_lastSpawnedFigure != null) 
            {
                ApplyRotation(_lastSpawnedFigure, domino);
                ApplyRotation(domino, _lastSpawnedFigure);
            }
            
            _lastSpawnedFigure = domino;
        }

        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }

        private void ApplyRotation(Transform current, Transform rotateTo)
        {
            current.LookAt(rotateTo);
            Vector3 newRotation = new Vector3(0f, current.eulerAngles.y, 0f);
            current.eulerAngles = newRotation;
        }
    }
}
