using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour, IEntity
{

    public int _Health { get; set; } = 1000;
    public GameObject HealthBar { get; set; } = null;
    public Action<int, string> OnDeath { get; set; }
    public Action<int, string> OnDamaged { get; set; }
    
    public float Speed;
    public float CameraSpeed;

    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _Gun;
    private CharacterController _Controller;

    private void Awake()
    {
        _Controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        Move();
        RotateCamera();
    }

    private void Move()
    {
        Vector3 movement = new Vector3();
        movement += transform.forward * Input.GetAxis("Vertical");
        movement += transform.right * Input.GetAxis("Horizontal");
        movement.Normalize();

        transform.position += movement * Speed * Time.deltaTime;
        /*_Controller.Move(movement * Speed * Time.deltaTime);*/
    }

    private void RotateCamera()
    {
        Transform cameraTransform = _Camera.transform;
        transform.RotateAround(cameraTransform.position, Vector3.up, Input.GetAxis("Mouse X") * CameraSpeed);
        cameraTransform.RotateAround(cameraTransform.position, cameraTransform.right, Input.GetAxis("Mouse Y") * CameraSpeed * -1);

        Quaternion cameraRotation = cameraTransform.rotation;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -88, 88);
        cameraTransform.rotation = cameraRotation;
        
        _Gun.transform.LookAt(cameraTransform.position + cameraTransform.forward * 10);
    }

    public void TakeDamage(int damage, string hitboxName)
    {
        throw new NotImplementedException();
    }

    public void SubToDeathEvent(Action<int, string> function)
    {
        throw new NotImplementedException();
    }

    public void SubToDamageEvent(Action<int, string> function)
    {
        throw new NotImplementedException();
    }
}
