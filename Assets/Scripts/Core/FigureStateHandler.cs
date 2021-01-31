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

        public event UnityAction LevelFailed;

        private void Awake()
        {
            HandleExistedFigures();
        }

        private void Update()
        {
            _timeSinceLastFigureFell += Time.deltaTime;
            
            if (_timeSinceLastFigureFell > _timeLimit)
                LevelFailed?.Invoke();
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
            LevelFailed?.Invoke();
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