using System;
using System.Collections.Generic;
using Figure;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class FigureStateHandler : MonoBehaviour
    {
        private List<DominoFigure> _handles = new List<DominoFigure>();
        private float _timeSinceLastFigureFell = 0;
        private float _timeLimit = 3f;

        private bool _isTimesOut = false;
        
        public event UnityAction TimesOut;

        private void Awake()
        {
            HandleExistedFigures();
        }

        private void Update()
        {
            if (_isTimesOut) return;
            
            _timeSinceLastFigureFell += Time.deltaTime;
            if (_timeSinceLastFigureFell > _timeLimit)
            {
                TimesOut?.Invoke();
                _isTimesOut = true;
            }
        }
        
        private void OnDisable()
        {
            UnsubscribeFromAllFigures();
        }

        public void HandleCreatedFigure(DominoFigure figure)
        {
            HandleFigure(figure);
        }
        
        private void HandleExistedFigures()
        {
            foreach (var figure in gameObject.GetComponentsInChildren<DominoFigure>())
                HandleFigure(figure);
        }

        private void HandleFigure(DominoFigure figure)
        {
            _handles.Add(figure);
            figure.FigureNotFellAndLeftScreen += OnFigureNotFellAndLeftScreen;
            figure.FigureFell += ResetFigureTimer;
        }

        private void OnFigureNotFellAndLeftScreen()
        {
            UnsubscribeFromAllFigures();
            TimesOut?.Invoke();
        }

        private void ResetFigureTimer()
        {
            _timeSinceLastFigureFell = 0;
        }

        private void UnsubscribeFromAllFigures()
        {
            foreach (var figure in _handles)
            {
                figure.FigureNotFellAndLeftScreen -= OnFigureNotFellAndLeftScreen;
                figure.FigureFell -= ResetFigureTimer;
            }
        }
    }
}