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

        public void Update()
        {
            if (Application.isPlaying) return;
            
            if (_generate)
            {
                _generate = false;
                GeneratePlaceableBlocks();
            }
            
            if (transform.childCount > 0)
                ChangeColorIfNotPlaceable();
        }

        private void GeneratePlaceableBlocks()
        {
            if (transform.childCount > 0)
                DeleteExistedBlocks();

            CreateNewBlocks();
        }

        private void DeleteExistedBlocks()
        {
            foreach (var block in GetComponentsInChildren<Block>())
                DestroyImmediate(block);
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
                }
            }
        }

        private void ChangeColorIfNotPlaceable()
        {
            foreach (var block in GetComponentsInChildren<Block>())
            {
                if (IsPlaceable(block.gameObject))
                    block.GetComponent<MeshRenderer>().material = _greenMaterial;
                else
                    block.GetComponent<MeshRenderer>().material = _redMaterial;
            }
        }

        private bool IsPlaceable(GameObject block)
        {
            return block.CompareTag("Ground");
        }
        
        
    }
}
#endif