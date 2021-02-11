using System;
using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(ColorChanger))]
    [RequireComponent(typeof(SFXPresenter))]
    [RequireComponent(typeof(FigureRotator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class DominoFigure : MonoBehaviour
    {
        public event Action<DominoFigure> FigureFellAndLeftScreen;
        public event Action FigureFell;

        private FigureRenderState _renderState = FigureRenderState.NotRendered;
        private FigurePositionState _positionState = FigurePositionState.Stay;
        
        private Rigidbody _rigidbody;
        private ColorChanger _colorChanger;
        private SFXPresenter _sfxPresenter;
        private FigureRotator _figureRotator;
        
        private float _fallingVelocityY = -0.3f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _colorChanger = GetComponent<ColorChanger>();
            _sfxPresenter = GetComponent<SFXPresenter>();
            _figureRotator = GetComponent<FigureRotator>();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.GetComponent<DominoFigure>())
                _sfxPresenter.PlaySound();
        }

        private void OnCollisionStay(Collision other)
        {
            if (_positionState == FigurePositionState.Fell) return;
            if (!other.collider.GetComponent<DominoFigure>()) return;
            if (_rigidbody.velocity.y > _fallingVelocityY) return;
            
            _positionState = FigurePositionState.Fell;
            FigureFell?.Invoke();
            _colorChanger.StartChangeColor();
        }
        
        private void OnBecameVisible()
        {
            _renderState = FigureRenderState.Rendered;
        }

        private void OnBecameInvisible()
        {
            if (_positionState == FigurePositionState.Fell && _renderState == FigureRenderState.Rendered)
            {
                FigureFellAndLeftScreen?.Invoke(this);
                ApplyDefaultStateAndColor();
            }
        }

        private void ApplyDefaultStateAndColor()
        {
            _positionState = FigurePositionState.Stay;
            _colorChanger.ChangeColorToDefault();
        }

        public void SetRotation(Transform rotateTo)
        {
            _figureRotator.SetRotation(rotateTo);
        }
    }
}
