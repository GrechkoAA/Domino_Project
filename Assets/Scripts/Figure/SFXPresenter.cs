using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AudioSource))]
    public class SFXPresenter : MonoBehaviour
    {
        private AudioSource _audioSource;

        public void PlaySound()
        {
            _audioSource.Play();
        }
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}
