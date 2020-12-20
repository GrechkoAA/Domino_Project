using System;
using Figure;
using UnityEngine;

namespace Ground
{
    public class FinishGround : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.GetComponent<DominoFigure>())
            {
                Debug.Log("WIN!");
            }
        }
    }
}