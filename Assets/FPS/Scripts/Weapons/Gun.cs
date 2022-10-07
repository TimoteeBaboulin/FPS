using System;
using System.Collections;
using FPS.Scripts.Weapons.Bullets.Interface;
using UnityEngine;

namespace FPS.Scripts
{
    [Serializable]
    public class Gun : MonoBehaviour
    {
        public Player player;

        public Animator GunAnimator;
        public Animator ReloadAnimator;
        //Need access to gun barrel
        //Need access to camera
        //Already have camera given by Shoot()'s public call
        //Use a public member to keep it in memory?
        [SerializeField] private GunStat _currentGun = null;
        [SerializeField] private int _currentMagasine = 30;
        private bool _canShoot = true;
        //the time at which the last shot was shot
        private float _nextShot = 0f;
    
        public GameObject Barrel;
        public GunStat CurrentGun
        {
            get => _currentGun;
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

                if (_currentGun.MagasineSize == -1) return;

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
                if (_canShoot && !IsOutOfAmmo && Time.time > _nextShot + _currentGun.ShotDelay) return true;
                return false;
            }
        }

        /// <summary>
        /// All the public access functions
        /// </summary>
        [ContextMenu("Shoot")]
        public void Shoot()
        {
            if (_currentGun == null) return;
            if (CanShoot)
            {
                if (_currentMagasine > -1)
                    _currentMagasine--;
                SpawnProjectile();
                PlayAnimation();
                _nextShot = Time.time + _currentGun.ShotDelay;
                return;
            }

            if (_currentMagasine == 0)
            {
                Reloading();
            }

        }

        [ContextMenu("Reload")]
        public void Reload()
        {
            if (_currentMagasine == -1 || _currentMagasine >= _currentGun.MagasineSize) return;
            Reloading();
        }
    
    
        private void SpawnProjectile(float x = 0, float y = 0)
        {
            //Spawn the physical projectile and the visual projectile
            
            GameObject shot = Instantiate(_currentGun.Projectile,
                transform.position + transform.forward * _currentGun.SpawnRange, transform.rotation);
            GameObject visualProjectile = Instantiate(_currentGun.VisualProjectile, Barrel.transform.position, transform.rotation);

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
                visualProjectile.transform.LookAt(transform.position + transform.forward * 100);
            else
                visualProjectile.transform.LookAt(hit.point);

            //Set the shot's owner to be the player
            shot.GetComponent<INewBullet>().Owner = player;

            //Sync the bullet so the visual bullet dies if the physical does
            shot.AddComponent<DestroySync>();
            shot.GetComponent<DestroySync>().SyncedObject = visualProjectile;
        }

        private void PlayAnimation()
        {
            if (GunAnimator == null) return;
            GunAnimator.Play("Gun Animation", 1 , _currentGun.ShotDelay);
        }

        private void Reloading()
        {
            _currentMagasine = _currentGun.MagasineSize;
            _nextShot = Time.time + _currentGun.ReloadTime;

            if (ReloadAnimator == null) return;
            ReloadAnimator.CrossFade("Reloading", 0);
            ReloadAnimator.speed = 1 / _currentGun.ReloadTime;
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
}
