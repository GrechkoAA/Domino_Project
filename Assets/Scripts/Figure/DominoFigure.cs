using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Transform))]
    public class DominoFigure : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Transform _transform;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _transform = GetComponent<Transform>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.GetComponent<DominoFigure>())
                _audioSource.Play();
        }

        public void ApplyRotation(Transform rotateTo)
        {
            _transform.LookAt(rotateTo);
            _transform.eulerAngles = new Vector3(0f, _transform.eulerAngles.y, 0f);
        }
    }
}
