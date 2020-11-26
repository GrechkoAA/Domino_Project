using System.Linq;
using UnityEngine;

namespace Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private DominoFigure _prefab;
        [SerializeField] private float _minimalDistanceBetweenFigures = 0.2f;
        [SerializeField] private Camera _camera;
        
        private Vector3 _spawnPosition;
        private DominoFigure _lastSpawnedFigure;

        private const string PlaceableGround = "Ground";

        private void Update()
        {
            if (Input.GetMouseButton(0))
                TryPlaceFigure();
        }

        private void TryPlaceFigure()
        {
            if (CanPlaceFigure())
              Spawn(_spawnPosition);
        }

        private bool CanPlaceFigure()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay(), Mathf.Infinity);
            if (hits.Length == 0) return false;

            foreach (var hit in hits)
            {
                if (!hit.collider.CompareTag(PlaceableGround)) continue;
                
                var sphereHits = Physics.SphereCastAll(hit.point, _minimalDistanceBetweenFigures, Vector3.up);
                if (sphereHits.Any(sphereHit => sphereHit.collider.GetComponent<DominoFigure>() != null))
                    return false;

                _spawnPosition = hit.point;
                return true;
            }
            
            return false;
        }

        private void Spawn(Vector3 position)
        {
            var domino = Instantiate(_prefab, position + Vector3.up, Quaternion.identity);
            
            if (_lastSpawnedFigure != null) 
            {
                _lastSpawnedFigure.ApplyRotation(domino.transform);
                domino.ApplyRotation(_lastSpawnedFigure.transform);
            }
            
            _lastSpawnedFigure = domino;
        }

        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
