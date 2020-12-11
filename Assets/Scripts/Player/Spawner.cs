using Figure;
using UnityEngine;

namespace Player
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private DominoFigure _prefab;
        [SerializeField] private float _lineBreakDistance = 1f;
        
        private DominoFigure _previousInstance;
        
        public void Spawn(Vector3 position)
        {
            var currentInstance = Instantiate(_prefab, position + Vector3.up, Quaternion.identity);

            if (_previousInstance != null)
                if (DistanceBetweenFigures(currentInstance.transform.position, _previousInstance.transform.position) < _lineBreakDistance)
                    RotateSpawnedFigures(currentInstance);

            _previousInstance = currentInstance;
        }
        
        private void RotateSpawnedFigures(DominoFigure current)
        {
            _previousInstance.ApplyRotation(current.transform);
            current.ApplyRotation(_previousInstance.transform);
        }
        
        private float DistanceBetweenFigures(Vector3 first, Vector3 second)
        {
            return Vector3.Distance(first, second);
        }
    }
}