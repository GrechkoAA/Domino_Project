using System;
using System.Collections.Generic;
using Figure;
using UnityEngine;

namespace Core
{
    public class FigureStateHandler : MonoBehaviour
    {
        private List<DominoFigure> _handles = new List<DominoFigure>();

        private void Awake()
        {
            HandleExistedFigures();
        }

        public void HandleCreatedFigure(DominoFigure figure)
        {
            HandleFigure(figure);
            Debug.Log(_handles.Count);
        }
        
        private void HandleExistedFigures()
        {
            foreach (var figure in gameObject.GetComponentsInChildren<DominoFigure>())
                HandleFigure(figure);
            
            Debug.Log(_handles.Count);
        }

        private void HandleFigure(DominoFigure figure)
        {
            _handles.Add(figure);
            figure.FigureStayAndLeftScreen += OnFigureStayAndLeftScreen;
        }

        private void OnFigureStayAndLeftScreen()
        {
            foreach (var figure in _handles)
            {
                figure.FigureStayAndLeftScreen -= OnFigureStayAndLeftScreen;
            }

            Debug.Log(gameObject.name + " GAMEOVER");
        }
    }
}