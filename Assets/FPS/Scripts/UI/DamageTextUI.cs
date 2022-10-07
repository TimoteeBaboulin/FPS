using UnityEngine;
using Random = UnityEngine.Random;

namespace FPS.Scripts.UI
{
    public class DamageTextUI : MonoBehaviour
    {
        public float TotalTime = 2;

        [SerializeField] private float _RigidbodyInitialSpeed = 1;
    
        [SerializeField] private float _HorizontalSpeed = 1;
        [SerializeField] private float _VerticalSpeed = 1;
    
        [SerializeField] private float _GravityScale = 1;

        private float _Timer;

        private void Start()
        {
            GetComponent<RectTransform>().localPosition += Vector3.up;
            GetComponent<RectTransform>().forward = Camera.main.gameObject.transform.forward;
            _Timer = 0;
        }
    
        private void OnEnable()
        {
            var rigidbody = GetComponent<Rigidbody>();
            Vector3 newVelocity = Vector3.zero;
            newVelocity.y = _VerticalSpeed;
            int rand = Random.Range(1, 3) * 2 - 3;
            newVelocity.x = rand * _HorizontalSpeed;
            rand = Random.Range(1, 3) * 2 - 3;
            newVelocity.z = rand * _HorizontalSpeed;
            rigidbody.AddForce(newVelocity * _RigidbodyInitialSpeed);
        }

        private void FixedUpdate()
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity += Vector3.up * Physics.gravity.y * _GravityScale * Time.deltaTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (Camera.main.gameObject.transform.hasChanged)
                GetComponent<RectTransform>().forward = Camera.main.gameObject.transform.forward;
            _Timer += Time.deltaTime;
            //if (_Timer >= TotalTime)
            //Destroy(gameObject);
        }
    }
}
