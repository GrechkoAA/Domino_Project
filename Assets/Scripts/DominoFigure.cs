using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DominoFigure : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<DominoFigure>())
        {
            _audioSource.Play();
        }
    }
}
