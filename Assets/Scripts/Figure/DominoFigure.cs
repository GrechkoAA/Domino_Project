﻿using System;
using System.Collections;
using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(MeshRenderer))]
    public class DominoFigure : MonoBehaviour
    {
        public event Action FigureNotFellAndLeftScreen;

        private bool _isFell;

        private FigureRenderState _state = FigureRenderState.NotRendered;
        
        private AudioSource _audioSource;
        private Transform _transform;
        private MeshRenderer _mesh;
        private Material _material;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _transform = GetComponent<Transform>();
            _mesh = GetComponent<MeshRenderer>();
        }

        private void OnCollisionStay(Collision other)
        {
            if (_isFell) return;
            if (!other.gameObject.CompareTag("Ground")) return;
            if (!(GetComponent<Rigidbody>().velocity.y < -0.2f)) return;
            
            _isFell = true;
            StartCoroutine(ChangeColor());
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.GetComponent<DominoFigure>())
                _audioSource.Play();
        }

        private void OnBecameVisible()
        {
            _state = FigureRenderState.Rendered;
        }

        private void OnBecameInvisible()
        {
            if (!_isFell && _state == FigureRenderState.Rendered)
                FigureNotFellAndLeftScreen?.Invoke();
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
