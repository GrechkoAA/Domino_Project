using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
namespace Ground
{
    [ExecuteAlways]
    public class PlaceableGround : MonoBehaviour
    {
        [SerializeField] private GameObject _cube;
        [SerializeField] private bool _generate;

        [SerializeField] private Material _greenMaterial;
        [SerializeField] private Material _redMaterial;
        
        private int _offset = 2;
        private int _maximumBlocks = 5;
        
        private List<GameObject> _blocks = new List<GameObject>();

        public void Update()
        {
            if (_generate)
            {
                _generate = false;
                GeneratePlaceableBlocks();
            }
            
            if (_blocks.Count > 0)
                ChangeColorIfNotPlaceable();
        }

        private void GeneratePlaceableBlocks()
        {
            if (_blocks.Count > 0)
                DeleteExistedBlocks();

            CreateNewBlocks();
        }

        private void DeleteExistedBlocks()
        {
            foreach (var block in _blocks)
                DestroyImmediate(block);

            _blocks = new List<GameObject>();
        }

        private void CreateNewBlocks()
        {
            for (int x = 0; x < _maximumBlocks; x++)
            {
                for (int z = 0; z < _maximumBlocks; z++)
                {
                    var instance = Instantiate(_cube, transform);
                    var localPosition = instance.transform.localPosition;
                    localPosition = new Vector3(localPosition.x + x * _offset, 0, localPosition.z - z * _offset);
                    instance.transform.localPosition = localPosition;
                    
                    _blocks.Add(instance);
                }
            }
        }

        private void ChangeColorIfNotPlaceable()
        {
            foreach (var block in _blocks)
            {
                if (IsPlaceable(block))
                    block.GetComponent<MeshRenderer>().material = _greenMaterial;
                else
                    block.GetComponent<MeshRenderer>().material = _redMaterial;
            }
        }

        private bool IsPlaceable(GameObject block)
        {
            Debug.Log(block.tag);
            return block.CompareTag("Ground");
        }
    }
}
#endif