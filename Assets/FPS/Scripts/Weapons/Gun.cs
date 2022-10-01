using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGun", menuName = "Gun", order = 0)]
public class Gun : ScriptableObject
{
    public Mesh GunMesh = null;
    public GunTypeEnum GunType = GunTypeEnum.Auto;
    public ShotTypeEnum ShotType = ShotTypeEnum.Normal;
    public GameObject Projectile = null;
    public GameObject VisualProjectile;
    public float ShotDelay = 0.5f;
    public float BulletDistance = 0;
    public int MagasineSize = 10;
    public int CurrentMagasine = 10;
    public float ReloadTime = 2f;
    public int PelletCount = 1;
    public float MaxAngle = 15;
    public int BurstFire = 3;

    private void Awake()
    {
        CurrentMagasine = MagasineSize;
    }
}
public enum  GunTypeEnum
{
    Auto = 0,
    Burst = 1,
    SemiAuto = 2
}

public enum ShotTypeEnum
{
    Normal = 0,
    ShotShell = 1
}