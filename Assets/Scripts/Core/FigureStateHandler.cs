using System;
using System.Collections.Generic;
using Figure;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    [ExecuteAlways]
    public class FigureStateHandler : MonoBehaviour
    {
        private List<DominoFigure> _handles = new List<DominoFigure>();

        public event UnityAction LevelFailed;

        private void Awake()
        {
            HandleExistedFigures();
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
        }

        private void OnFigureNotFellAndLeftScreen()
        {
            foreach (var figure in _handles)
                figure.FigureNotFellAndLeftScreen -= OnFigureNotFellAndLeftScreen;

            LevelFailed?.Invoke();
        }
    }
}