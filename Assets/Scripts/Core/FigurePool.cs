using System;
using System.Collections.Generic;
using Figure;
using UnityEngine;

namespace Core
{
    public class FigurePool : MonoBehaviour
    {
        [SerializeField] private DominoFigure _prefab;
        [SerializeField] private int _poolLimit = 100;
        
        private Queue<DominoFigure> _availableObjects = new Queue<DominoFigure>();

        private void Awake()
        {
            GrowPool();
        }

        public DominoFigure GetObject()
        {
            var instance = _availableObjects.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnObject(DominoFigure instance)
        {
            instance.gameObject.SetActive(false);
            _availableObjects.Enqueue(instance);
            
        }

        private void GrowPool()
        {
            for (int i = 0; i < _poolLimit; i++)
            {
                var instance = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
                instance.gameObject.SetActive(false);
                _availableObjects.Enqueue(instance);
            }
        }
        
    }
}