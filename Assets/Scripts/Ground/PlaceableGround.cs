using UnityEngine;

#if UNITY_EDITOR
namespace Ground
{
    [ExecuteAlways]
    public class PlaceableGround : MonoBehaviour
    {
        [SerializeField] private Material _greenMaterial;
        [SerializeField] private Material _redMaterial;

        public void Update()
        {
            if (Application.isPlaying) return;

            if (transform.childCount > 0)
                TryChangeColor();
        }

        private void TryChangeColor()
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