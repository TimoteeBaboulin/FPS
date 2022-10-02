using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class NewGun
{
    //Need access to gun barrel
    //Need access to camera
    //Already have camera given by Shoot()'s public call
    //Use a public member to keep it in memory?
    [SerializeField] private GunStat _currentGun = null;
    [SerializeField] private int _currentMagasine = 30;
    private bool _canShoot = true;
    private float _lastShot = 0f;

    public Camera Camera;
    public GameObject Barrel;

    public GunStat CurrentGun
    {
        get => _currentGun;
        set
        {
            if (_currentGun == value) return;
            _currentGun = value;
            if (value.MagasineSize <= 0) _currentMagasine = -1;
            else _currentMagasine = value.MagasineSize;
        }
    }
    public int CurrentMagasine
    {
        get => _currentMagasine;
        set
        {
            if (value > _currentGun.MagasineSize)
            {
                _currentMagasine = _currentGun.MagasineSize;
                return;
            }

            if (_currentGun.MagasineSize <= 0) return;

            _currentMagasine = value;
        }
    }
    public bool IsOutOfAmmo
    {
        get
        {
            if (_currentGun.MagasineSize <= 0) return false;
            if (_currentMagasine > 0) return false;
            return true;
        }
    }
    public bool CanShoot
    {
        get
        {
            if (_canShoot && !IsOutOfAmmo && Time.time > _lastShot + _currentGun.ShotDelay) return true;
            return false;
        }
    }

    
    
    /// <summary>
    /// All the public access functions
    /// </summary>
    public void Shoot()
    {
        Debug.Log(CanShoot);
        if (_currentGun == null) return;
        if (!CanShoot) return; 
        SpawnProjectile();
        _lastShot = Time.time;
    }
    
    
    private void SpawnProjectile(float x = 0, float y = 0)
    {
        Transform cameraTransform = Camera.transform;
        //Spawn the physical projectile and the visual projectile
        GameObject shot = GameObject.Instantiate(_currentGun.Projectile,
            cameraTransform.position + cameraTransform.forward * _currentGun.SpawnRange, cameraTransform.rotation) as GameObject;
        GameObject visualProjectile = GameObject.Instantiate(_currentGun.VisualProjectile, Barrel.transform.position, cameraTransform.rotation);

        if (x != 0 || y != 0)
        {
            Vector3 euler = shot.transform.rotation.eulerAngles;
            euler.x += x;
            euler.y += y;
            shot.transform.rotation = Quaternion.Euler(euler);
        }

        //Proceed to shoot a ray to find where the bullet should reach
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(shot.transform.position, shot.transform.forward, out hit, 100,
            LayerMask.GetMask("Hitbox", "Terrain"));
        //If we didn't find a target, just shoot towards the middle
        //If we did find, shoot toward the point of the hitbox that was hit
        if (hit.collider == null)
            visualProjectile.transform.LookAt(cameraTransform.position + cameraTransform.forward * 100);
        else
            visualProjectile.transform.LookAt(hit.point);

        //Set the shot's owner to be the player
        // shot.GetComponent<INewBullet>().Owner = GetComponentInParent<Player>();

        //Sync the bullet so the visual bullet dies if the physical does
        shot.AddComponent<DestroySync>();
        shot.GetComponent<DestroySync>().SyncedObject = visualProjectile;
    }
    
    
    /// <summary>
    /// All the coroutines needed for the code
    /// </summary>
    IEnumerator TimerCoroutine()
    {
        _canShoot = false;
        float timer = 0;
        while (timer < _currentGun.ShotDelay)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        timer = 0;
        _canShoot = true;
    }
}
