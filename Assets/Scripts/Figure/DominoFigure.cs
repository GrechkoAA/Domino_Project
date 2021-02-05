﻿using System;
using System.Collections;
using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Rigidbody))]
    public class DominoFigure : MonoBehaviour
    {
        public event Action<DominoFigure> FigureFellAndLeftScreen;
        public event Action FigureFell;

        private bool _isFell;

        private FigureRenderState _state = FigureRenderState.NotRendered;
        
        private AudioSource _audioSource;
        private Transform _transform;
        private MeshRenderer _mesh;
        private Material _material;
        private Rigidbody _rigidbody;
        
        private Color _defaultColor;
        private float _fallingVelocityY = -0.3f;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _transform = GetComponent<Transform>();
            _mesh = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();

            _defaultColor = _mesh.material.color;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.GetComponent<DominoFigure>())
                _audioSource.Play();
        }
        
        private void OnCollisionStay(Collision other)
        {
            if (_isFell) return;
            if (!other.collider.GetComponent<DominoFigure>()) return;
            if (_rigidbody.velocity.y > _fallingVelocityY) return;
            
            _isFell = true;
            FigureFell?.Invoke();
            StartCoroutine(ChangeColor());
        }
        
        private void OnBecameVisible()
        {
            _state = FigureRenderState.Rendered;
        }

        private void OnBecameInvisible()
        {
            if (_isFell && _state == FigureRenderState.Rendered)
            {
                ChangeToDefault();
                FigureFellAndLeftScreen?.Invoke(this);
            }
        }

        private void ChangeToDefault()
        {
            _material.color = _defaultColor;
            _isFell = false;
        }

        public void ApplyRotation(Transform rotateTo)
        {
            _transform.LookAt(rotateTo);
            _transform.eulerAngles = new Vector3(0f, _transform.eulerAngles.y, 0f);
        }

        private IEnumerator ChangeColor()
        {
            _material = _mesh.material;
            while (_material.color != Color.yellow)
            {
                _material.color = Color.Lerp(_material.color, Color.yellow, 0.05f);
                yield return new WaitForSeconds(0f);
            }
        }
    }
}
