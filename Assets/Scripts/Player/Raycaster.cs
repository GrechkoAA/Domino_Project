using System;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

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
                var k = Physics.SphereCastAll(hit.point, 0.1f, Vector3.up);
                foreach (var x in k)
                {
                    if (x.collider.GetComponent<DominoFigure>() != null) return false;
                }
            }

            _spawnPosition = hits[0].point;
            return true;
        }

        private void Spawn(Vector3 position)
        {
            var domino = Instantiate(_prefab, position + Vector3.up, Quaternion.identity);
            
            if (_lastSpawnedFigure != null)
            {
                _lastSpawnedFigure.LookAt(domino.transform);
                _lastSpawnedFigure.eulerAngles = new Vector3(0f, _lastSpawnedFigure.transform.eulerAngles.y, 0f);
                domino.transform.LookAt(_lastSpawnedFigure);
                domino.transform.eulerAngles = new Vector3(0f, domino.transform.eulerAngles.y, 0f);
            }
            
            _lastSpawnedFigure = domino.transform;
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
