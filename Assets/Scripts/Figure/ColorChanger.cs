using System.Collections;
using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ColorChanger : MonoBehaviour
    {
        public Color CurrentColor { get; private set; }
        
        private MeshRenderer _mesh;
        private Color _defaultColor;

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
        }
        
        private void Start()
        {
            _defaultColor = _mesh.material.color;
            CurrentColor = _defaultColor;
        }

        private void OnBecameInvisible()
        {
            ChangeColorToDefault();
        }
        
        public void StartChangeColor()
        {
            StartCoroutine(ChangeColor());
        }
        
        public void ChangeColorToDefault()
        {
            _mesh.material.color = _defaultColor;
            CurrentColor = _defaultColor;
        }
        
        private IEnumerator ChangeColor()
        {
            while (_mesh.material.color != Color.yellow)
            {
                CurrentColor = Color.Lerp(_mesh.material.color, Color.yellow, 0.05f); 
                _mesh.material.color = CurrentColor;
                yield return new WaitForSeconds(0f);
            }
        }
    }
}
