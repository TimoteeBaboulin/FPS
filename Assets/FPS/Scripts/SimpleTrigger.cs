using Unity.Mathematics;
using UnityEngine;

namespace FPS.Scripts
{
    public class SimpleTrigger : MonoBehaviour
    {
        private Camera _TargetCamera;
        private float _Timer;
    
        [SerializeField] private Color _Base = Color.black;
        [SerializeField] private Color _Middle = Color.cyan;

        private void Start()
        {
            _Timer = 0;
            _TargetCamera = null;
        }

        private void Update()
        {
            if (!_TargetCamera) return;

            UpdateColor();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger");
            if (other.CompareTag("Player"))
                _TargetCamera = Camera.main;
        }

        private void OnTriggerExit(Collider other)
        {
            _TargetCamera.backgroundColor = _Base;
            Start();
        }

        void UpdateColor()
        {
            float sine = math.sin(_Timer * math.PI);
            sine = sine / 2;
            sine += 0.5f;

            _TargetCamera.backgroundColor = Color.Lerp(_Base, _Middle, sine);

            _Timer += Time.deltaTime;
        }
    }
}
