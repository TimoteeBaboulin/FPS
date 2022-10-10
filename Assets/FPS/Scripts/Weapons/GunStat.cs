using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FPS.Scripts
{
    [CreateAssetMenu(fileName = "newGun", menuName = "Gun", order = 0)]
    public class GunStat : ScriptableObject
    {
        /// <summary>
        /// Everything here has no setter, it must but Serialized through Unity's Scriptable Object System
        /// In order to change these stats in calculations, an override structure will be introduced
        /// </summary>
        [FormerlySerializedAs("_gunMesh")] [SerializeField] private Mesh _simplifiedMesh = null;
        [SerializeField] private GunTypeEnum _gunType = GunTypeEnum.Auto;
        [SerializeField] private ShotTypeEnum _shotType = ShotTypeEnum.Normal;
        [SerializeField] private GameObject _projectile = null;
        [SerializeField] private GameObject _visualProjectile = null;
        [SerializeField] private float _shotDelay = 0.5f;
        [SerializeField] private float _range = 0;
        [SerializeField] private float _spawnRange = 0;
        [SerializeField] private int _magasineSize = 10;
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private int _pelletCount = 3;
        [SerializeField] private float _maxAngle = 15;
        [SerializeField] private int _burstFire = 3;
        [SerializeField] private float _sight = 1;

        public Mesh SimplifiedMesh
        {
            get => _simplifiedMesh;
        }
        public GunTypeEnum GunType
        {
            get => _gunType;
        }
        public ShotTypeEnum ShotType
        {
            get => _shotType;
        }
        public GameObject Projectile
        {
            get => _projectile;
        }
        public GameObject VisualProjectile
        {
            get => _visualProjectile;
        }
        public float ShotDelay
        {
            get => _shotDelay;
        }
        public float Range
        {
            get => _range;
        }
        public float SpawnRange
        {
            get => _spawnRange;
        }
        public int MagasineSize
        {
            get => _magasineSize;
        }
        public float ReloadTime
        {
            get => _reloadTime;
        }
        public int PelletCount
        {
            get => _pelletCount;
        }
        public float MaxAngle
        {
            get => _maxAngle;
        }
        public int BurstFire
        {
            get => _burstFire;
        }
        public float Sight
        {
            get => _sight;
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
}