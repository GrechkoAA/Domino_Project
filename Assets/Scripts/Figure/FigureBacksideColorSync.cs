using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FigureBacksideColorSync : MonoBehaviour
    {
        private DominoFigure _frontSideFigure;
        private MeshRenderer _backSideRenderer;

        private void Awake()
        {
            _frontSideFigure = GetComponentInParent<DominoFigure>();
            _backSideRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            _backSideRenderer.material.color = _frontSideFigure.CurrentColor;
        }
    }
}
