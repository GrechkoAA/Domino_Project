using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Raycaster))]
    public class PlayerInput : MonoBehaviour
    {
        private Raycaster _raycaster;

        private void Awake()
        {
            _raycaster = GetComponent<Raycaster>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                _raycaster.TryPlaceFigure();
        }
    }
}