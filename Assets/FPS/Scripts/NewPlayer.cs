using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewPlayer : MonoBehaviour
{
    [Range(0.1f, 100)]
    public float Speed = 1;

    [SerializeField] private float _cameraSpeed = 1;
    public float CameraSpeed
    {
        get => _cameraSpeed;
    }

    [SerializeField] private NewGun _gun = null;
    [SerializeField] private GameObject _camera = null;
    [SerializeField] private GameObject _gunGO = null;
    private CharacterController _controller = null;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_camera == null) _camera = Camera.main.gameObject;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateCamera();
        Shoot();
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
        Transform cameraTransform = _camera.transform;
        transform.RotateAround(cameraTransform.position, Vector3.up, Input.GetAxis("Mouse X") * CameraSpeed);
        cameraTransform.RotateAround(cameraTransform.position, cameraTransform.right, Input.GetAxis("Mouse Y") * CameraSpeed * -1);

        Quaternion cameraRotation = cameraTransform.rotation;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -88, 88);
        cameraTransform.rotation = cameraRotation;
        
        _gunGO.transform.LookAt(cameraTransform.position + cameraTransform.forward * 10);
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            _gun.Shoot();
        }
    }
}
