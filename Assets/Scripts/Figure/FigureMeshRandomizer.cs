using UnityEngine;
using Random = UnityEngine.Random;

namespace Figure
{
    [RequireComponent(typeof(MeshFilter))]
    public class FigureMeshRandomizer : MonoBehaviour
    {
        [SerializeField] private Mesh[] _figureMeshes;
        [SerializeField] private MeshFilter _backSideFilter;
        
        private MeshFilter _frontMesh;
        private MeshFilter _backMesh;

        private void Awake()
        {
            _frontMesh = GetComponent<MeshFilter>();
            _backMesh = GetComponentInChildren<MeshFilter>();
        }

        private void Start()
        {
            _frontMesh.mesh = GetRandomMesh();
            ApplyFrontMeshToBack();
        }

        private Mesh GetRandomMesh()
        {
            return _figureMeshes[Random.Range(0, _figureMeshes.Length)];
        }

        private void ApplyFrontMeshToBack()
        {
            _backSideFilter.mesh = _frontMesh.mesh;
        }
    }
}