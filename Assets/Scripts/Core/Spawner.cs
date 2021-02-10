using Figure;
using UnityEngine;
namespace Core
{
[ExecuteAlways]

[RequireComponent(typeof(FigurePool))]
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _lineBreakDistance = 1f;
        [SerializeField] private FigureStateHandler _figureHandler;

        private FigurePool _pool;
        private DominoFigure _previousInstance;

        private void Awake()
        {
            _pool = GetComponent<FigurePool>();
        }

        public void Spawn(Vector3 position)
        {
            var currentInstance = _pool.GetObject(position + Vector3.up);
            currentInstance.transform.SetParent(_figureHandler.transform);

            if (_previousInstance != null)
                if (DistanceBetweenFigures(currentInstance.transform.position, _previousInstance.transform.position) < _lineBreakDistance)
                    RotateSpawnedFigures(currentInstance);
            
            _figureHandler.HandleCreatedFigure(currentInstance);

            currentInstance.FigureFellAndLeftScreen += Despawn;
            
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

        private void Despawn(DominoFigure instance)
        {
            instance.FigureFellAndLeftScreen -= Despawn;
            
            _pool.ReturnObject(instance);
        }
    }
}