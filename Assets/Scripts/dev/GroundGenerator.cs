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
        [SerializeField] private GameObject _placeableGround;
        [SerializeField] private bool _place;
        [SerializeField] private bool _deleteAll;

        private List<GameObject> _createdObjectPool = new List<GameObject>();

        private void Start()
        {
            _place = false;
            _deleteAll = false;
        }
        
        private void Update()
        {
            if (Application.isEditor)
            {
                if (_place)
                    CreateObjects();
                if (_deleteAll)
                    DeleteObjects();
            }
        }

        private void CreateObjects()
        {
            _place = false;

            DeleteAlreadyExistedGrounds();
            
            for (int i = 1; i <= _sections; i++)
                PlaceGroundBlock(i);

            if (_placeableSections.Length <= 0) return;

            for (int j = 0; j < _placeableSections.Length; j++)
                ChangeGroundToPlaceable(j);
        }

        private void DeleteAlreadyExistedGrounds()
        {
            if (_createdObjectPool.Count > 0)
                DeleteObjects();
        }

        private void DeleteObjects()
        {
            _deleteAll = false;

            if (_createdObjectPool.Count <= 0) return;
            
            foreach (var ground in _createdObjectPool)
                DestroyImmediate(ground);

            _createdObjectPool = new List<GameObject>();
        }

        private void PlaceGroundBlock(int offset)
        {
            Vector3 newPosition = GetGroundPosition(offset);
            var instance = Instantiate(_ground, newPosition, Quaternion.identity);
            _createdObjectPool.Add(instance);
        }

        private void ChangeGroundToPlaceable(int index)
        {
            DestroyImmediate(_createdObjectPool[_placeableSections[index] - 1]);
            _createdObjectPool.RemoveAt(_placeableSections[index] - 1);
            Vector3 newPosition = GetGroundPosition(_placeableSections[index]);
            var instance = Instantiate(_placeableGround, newPosition, Quaternion.identity);
            _createdObjectPool.Insert(_placeableSections[index] - 1, instance);
        }

        private Vector3 GetGroundPosition(int offset)
        {
            return new Vector3(transform.position.x + GetComponent<MeshRenderer>().bounds.size.x * offset, transform.position.y, transform.position.z);
        }
    }
}
