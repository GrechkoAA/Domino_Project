using System;
using System.Collections.Generic;
using UnityEngine;

namespace dev
{
    [ExecuteInEditMode]
    public class GroundGenerator : MonoBehaviour
    {
        [SerializeField] private int _sections;
        [SerializeField] private int[] _placeableSections;

        [SerializeField] private GameObject _ground;
        [SerializeField] private bool _place;
        [SerializeField] private bool _deleteAll;

        private List<GameObject> _createdObjectPool;
        
        private void Update()
        {
            if (_place)
                CreateObjects();
            if (_deleteAll)
                DeleteObjects();
        }

        private void CreateObjects()
        {
            _place = false;
            
            if (_createdObjectPool.Count > 0)
                DeleteObjects();
            
            for (int i = 1; i <= _sections; i++)
            {
                Vector3 newPosition = new Vector3(transform.position.x + GetComponent<MeshRenderer>().bounds.size.x * i, transform.position.y, transform.position.z);
                var instance = Instantiate(_ground, newPosition, Quaternion.identity);
                _createdObjectPool.Add(instance);
            }
        }

        private void DeleteObjects()
        {
            _deleteAll = false;

            foreach (var obj in _createdObjectPool)
                DestroyImmediate(obj.gameObject);
        }
    }
}
