using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace FPS.Scripts
{
    public class Player : MonoBehaviour
    {
        [Range(0.1f, 100)]
        public float Speed = 1;

        [SerializeField] private float _cameraSpeed = 1;
        public float CameraSpeed
        {
            get => _cameraSpeed;
        }

        [SerializeField] private GameObject _gun = null;
        [SerializeField] private GameObject _camera = null;
        [SerializeField] private TemporaryIK _IKScript = null;
        private CharacterController _controller = null;

        public GameObject Gun
        {
            get => _gun;
            set
            {
                if (_camera == null) return;
                _gun = Instantiate(value, _camera.transform);
                SetIK();
            }
        }
    
        void Start()
        {
            _controller = GetComponent<CharacterController>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (_camera == null) return;
            Camera.SetupCurrent(_camera.GetComponent<Camera>());
            if (_gun != null) Gun = _gun;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            RotateCamera();

            if (_gun == null || _gun.GetComponent<Gun>() == null) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                _gun.GetComponent<Gun>().Shoot();
                return;
            }
            
            if (Input.GetMouseButton(0) && _gun.GetComponent<Gun>().CurrentGun.GunType != GunTypeEnum.SemiAuto)
            {
                _gun.GetComponent<Gun>().Shoot();
                return;
            }

            if (Input.GetButtonDown("Reloading"))
            {
                _gun.GetComponent<Gun>().Reload();
                return;
            }
        }

        void FixedUpdate()
        {
            RaycastHit hit;
            Physics.Raycast(new Ray(_camera.transform.position, _camera.transform.forward), out hit, 100,
                LayerMask.NameToLayer("Terrain"));
            if (Gun == null) return;
            Gun.transform.LookAt(hit.point);
        }

        private void Move()
        {
            Vector3 movement = new Vector3();
            movement += transform.forward * Input.GetAxis("Vertical");
            movement += transform.right * Input.GetAxis("Horizontal");
            movement.Normalize();

            _controller.Move(movement * Speed * Time.deltaTime);
        }
    
        private void RotateCamera()
        {
            Transform cameraTransform = Camera.main.transform;
            transform.RotateAround(cameraTransform.position, Vector3.up, Input.GetAxis("Mouse X") * CameraSpeed);
            cameraTransform.RotateAround(cameraTransform.position, cameraTransform.right, Input.GetAxis("Mouse Y") * CameraSpeed * -1);

            Quaternion cameraRotation = cameraTransform.rotation;
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, -88, 88);
            cameraTransform.rotation = cameraRotation;
        }

        [ContextMenu("SetIK")]
        private void SetIK()
        {
            var links = _gun.GetComponent<GunIKLink>();
            if (_IKScript == null) return;
            if (links.RightHandIK != null)
            {
                _IKScript.RightHandObj = links.RightHandIK.transform;
            } else
            {
                _IKScript.RightHandObj = null;
            }
                    
            if (links.RightHandHint != null)
            {
                _IKScript.RightHandHint = links.RightHandHint.transform;
            } else
            {
                _IKScript.RightHandHint = null;
            }
            
            if (links.LeftHandIK != null)
            {
                _IKScript.LeftHandObj = links.LeftHandIK.transform;
            } else
            { 
                _IKScript.LeftHandObj = null;
            }
            
            if (links.LeftHandHint != null)
            {
                _IKScript.LeftHandHint = links.LeftHandHint.transform;
            } else
            {
                _IKScript.LeftHandHint = null;
            }
        }

        public void ChangeWeapon(GameObject gunPrefab)
        {
            if (_camera == null) return;
            if (_gun != null) Destroy(_gun);
            _gun = Instantiate(gunPrefab, _camera.transform);
            SetIK();
        }
    }

    [Serializable]
    public struct PlayerIKs
    {
        public GameObject LeftIKs;
        public GameObject RightIKs;
    }
}
