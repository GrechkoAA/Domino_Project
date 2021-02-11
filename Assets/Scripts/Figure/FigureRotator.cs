using UnityEngine;

namespace Figure
{
    [RequireComponent(typeof(Transform))]
    public class FigureRotator : MonoBehaviour
    {
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        public void SetRotation(Transform rotateTo)
        {
            _transform.LookAt(rotateTo);
            _transform.eulerAngles = new Vector3(0f, _transform.eulerAngles.y, 0f);
        }
    }
}