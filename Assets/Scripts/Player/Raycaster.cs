using System.Linq;
using Figure;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Spawner))]
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _minimalDistanceBetweenFigures = 0.2f;

        private Spawner _spawner;
        
        private Vector3 _spawnPosition;

        private const string PlaceableGround = "Ground";

        private void Awake()
        {
            _spawner = GetComponent<Spawner>();
        }

        public void TryPlaceFigure()
        {
            if (CanPlaceFigure())
              _spawner.Spawn(_spawnPosition);
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

        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
