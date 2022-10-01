using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class NewGun
{
    [SerializeField] private GunStat _currentGun = null;
    private int _currentMagasine = 0;
    private bool _canShoot = true;

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
            if (_canShoot && !IsOutOfAmmo) return true;
            return false;
        }
    }

    public void Shoot(Camera camera)
    {
        if (_currentGun == null) return;
        GameObject.Instantiate(_currentGun.Projectile, camera.transform.position, camera.transform.rotation);
    }
}
