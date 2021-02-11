using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(ColorChanger))]
    [RequireComponent(typeof(MeshRenderer))]
    public class FigureBacksideColorSync : MonoBehaviour
    {
        private ColorChanger _colorChanger;
        private MeshRenderer _backSideRenderer;

        private void Awake()
        {
            _colorChanger = GetComponentInParent<ColorChanger>();
            _backSideRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            _backSideRenderer.material.color = _colorChanger.CurrentColor;
        }
    }
}
