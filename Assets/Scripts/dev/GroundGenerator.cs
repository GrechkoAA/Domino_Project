using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

namespace dev
{
    [ExecuteInEditMode]
    public class GroundGenerator : MonoBehaviour
    {
        [SerializeField] private int _sections;
        [SerializeField] private int[] _placeableSections;

        [SerializeField] private GameObject _ground;
        [SerializeField] private GameObject _placeableGround;
        [SerializeField] private GameObject _finishGround;
        [SerializeField] private bool _place = false;
        [SerializeField] private bool _deleteAll = false;

        private List<GameObject> _createdObjectPool = new List<GameObject>(); 

        private void Awake()
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

            PlaceGroundFinish();
        }

        private void DeleteAlreadyExistedGrounds()
        {
            if (_createdObjectPool.Count > 0)
                DeleteObjects();
        }

        private void DeleteObjects()
        {
            _deleteAll = false;

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                if (transform.GetChild(i).CompareTag("NonDelete")) continue;
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            
            _createdObjectPool = new List<GameObject>();
        }

        private void PlaceGroundBlock(int offset)
        {
            Vector3 newPosition = GetGroundPosition(offset);
            var instance = Instantiate(_ground, newPosition, Quaternion.identity);
            instance.transform.SetParent(transform);
            _createdObjectPool.Add(instance);
        }

        private void ChangeGroundToPlaceable(int index)
        {
            DestroyImmediate(_createdObjectPool[_placeableSections[index] - 1]);
            _createdObjectPool.RemoveAt(_placeableSections[index] - 1);
            Vector3 newPosition = GetGroundPosition(_placeableSections[index]);
            var instance = Instantiate(_placeableGround, newPosition, Quaternion.identity);
            instance.transform.SetParent(transform);
            _createdObjectPool.Insert(_placeableSections[index] - 1, instance);
        }

        private void PlaceGroundFinish()
        {
            var instance = Instantiate(_finishGround, GetGroundPosition(_createdObjectPool.Count + 1), Quaternion.identity);
            instance.transform.SetParent(transform);
            _createdObjectPool.Add(instance);
        }

        private Vector3 GetGroundPosition(int offset)
        {
            return new Vector3(transform.position.x + GetComponent<MeshRenderer>().bounds.size.x * offset, transform.position.y, transform.position.z);
        }
    }
}

#endif
