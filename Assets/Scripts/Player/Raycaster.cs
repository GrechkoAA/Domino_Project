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
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), 0.3f, Mathf.Infinity);
            if (hits.Length == 0) return false;
            
            foreach (var hit in hits)
            {
                if (!hit.collider.CompareTag("Ground") || hit.collider.GetComponent<DominoFigure>() != null)
                    return false;
            }

            _spawnPosition = hits[0].point;
            return true;
        }

        private void Spawn(Vector3 position)
        {
            var domino = Instantiate(_prefab, position + Vector3.up, Quaternion.identity);

            if (_lastSpawnedFigure != null)
            {
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
