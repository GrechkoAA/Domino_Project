using UnityEngine;

namespace Pusher
{
    public class PusherRotater : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _speed = 150f;

        private void Update()
        {
            transform.RotateAround(_pivot.position, _pivot.forward, _speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}
