using System;
using Figure;
using UnityEngine;

namespace Ground
{
    public class FinishGround : MonoBehaviour
    {
        public event Action LevelFinished;

        private bool _isLevelFinished;

        private void OnCollisionEnter(Collision other)
        {
            if (_isLevelFinished) return;
            
            if (other.collider.GetComponent<DominoFigure>())
            {
                _isLevelFinished = true;
                LevelFinished?.Invoke();
            }
        }
    }
}