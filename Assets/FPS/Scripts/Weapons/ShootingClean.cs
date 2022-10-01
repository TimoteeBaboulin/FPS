using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ShootingClean : MonoBehaviour
{
    [SerializeField] private Gun _GunScript;
    [SerializeField] private Text _UIAmmo;
    [SerializeField] private Animator _ReloadingAnimator;
    [SerializeField] private Transform _GunBarrel;
    public GameObject Gun;

    private bool _LookingDownSight;
    private bool _BurstFireRunning = false;
    /*private GunState _State;*/
    
    private bool _CanShoot = true;
    
    // Start is called before the first frame update
    void Start()
    {
        /*_State = GunState.IDLE;*/
        UpdateUI();
        _CanShoot = true;
        _LookingDownSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Si tu ne peut pas tirer, ou que tu n'appuies sur aucun bouton
        if (!_CanShoot || (!Input.GetButton("Fire1") && !Input.GetButton("Reloading") && !Input.GetButton("Fire2")))
            return;

        if (Input.GetButton("Reloading"))
        {
            if (_GunScript.CurrentMagasine < _GunScript.MagasineSize && _GunScript.MagasineSize > 0)
            {
                StartCoroutine(ReloadCoroutine());
                return;
            }
        }

        if (Input.GetButtonDown("Fire2") && !_LookingDownSight)
        {
            StartCoroutine(LookingDownSightCoroutine());
            return;
        }
        
        if (Input.GetButton("Fire1"))
        {
            if (_GunScript.CurrentMagasine <= 0 && _GunScript.MagasineSize > 0)
            {
                StartCoroutine(ReloadCoroutine());
                return;
            }

            Fire();
            return;
        }
    }

    private bool Fire()
    {
        _GunScript.CurrentMagasine--;
        UpdateUI();
        
        switch (_GunScript.GunType)
        {
            case GunTypeEnum.Auto:
                SpawnBullet();
                StartCoroutine(TimerCoroutine());
                break;
            
            case GunTypeEnum.SemiAuto:
                if (!Input.GetButtonDown("Fire1")) return false;
                SpawnBullet();
                StartCoroutine(TimerCoroutine());
                break;
                
            case GunTypeEnum.Burst:
                StartCoroutine(BurstFire(3, 0.1f));
                break;
        }

        return true;
    }

    private void SpawnBullet() {
        switch (_GunScript.ShotType)
        {
            case ShotTypeEnum.Normal:
                SpawnProjectile();
                break;
            
            case ShotTypeEnum.ShotShell:
                SpawnPellets(10, 8);
                break;
        }
        
        Gun.GetComponent<Animator>().CrossFade("Fire Layer.Gun Animation", 0, 1);
    }

    private void SpawnPellets(int number, int maxAngle) {
        for (int x = 0; x < number; x++) {
            //Use a 2D circle to generate a set of 2 random values
            //that are directly linked to each other in a circle
            float distance = Random.Range(0f, 1f);
            int angle = Random.Range((int)0, 360);

            Vector2 randomPoint = new Vector2(distance * math.cos(angle),
                distance * math.sin(angle));
            randomPoint *= maxAngle;
            
            SpawnProjectile(randomPoint.x, randomPoint.y);
        }
    }

    private void SpawnProjectile(float x = 0, float y = 0)
    {
        //Spawn the physical projectile and the visual projectile
        GameObject shot = Instantiate(_GunScript.Projectile, transform.position + transform.forward * _GunScript.BulletDistance, transform.rotation);
        GameObject visualProjectile = Instantiate(_GunScript.VisualProjectile, _GunBarrel.position, transform.rotation);

        if (x != 0 || y != 0)
        {
            Vector3 euler = shot.transform.rotation.eulerAngles;
            euler.x += x;
            euler.y += y;
            shot.transform.rotation = Quaternion.Euler(euler);
        }
        
        //Proceed to shoot a ray to find where the bullet should reach
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(shot.transform.position, shot.transform.forward, out hit, 100, LayerMask.GetMask("Hitbox", "Terrain"));
        //If we didn't find a target, just shoot towards the middle
        //If we did find, shoot toward the point of the hitbox that was hit
        if (hit.collider == null)
            visualProjectile.transform.LookAt(transform.position + transform.forward * 100);
        else
            visualProjectile.transform.LookAt(hit.point);
                
        //Set the shot's owner to be the player
        shot.GetComponent<INewBullet>().Owner = GetComponentInParent<Player>();
        shot.AddComponent<DestroySync>();
        shot.GetComponent<DestroySync>().SyncedObject = visualProjectile;
    }

    private void UpdateUI()
    {
        if (_GunScript.MagasineSize <= 0) {
            _UIAmmo.text = "Infinite";
            return;
        }

        _UIAmmo.text = _GunScript.CurrentMagasine + "/" + _GunScript.MagasineSize;
    }
    
    //public access methods
    public bool ChangeGun(Gun newGun)
    {
        _GunScript = newGun;
        return true;
    }
    
    
    //Coroutines
    IEnumerator BurstFire(int shotAmount, float delay)
    {
        _CanShoot = false;
        int shotCount = 0;
        float timer = 0;

        while (shotCount < shotAmount)
        {
            SpawnBullet();
            shotCount++;

            timer = 0;
            while (timer < delay)
            {
                yield return null;
                timer += Time.deltaTime;
            }
        }

        StartCoroutine(TimerCoroutine());

        /*while (_BurstFireRunning)
        {
            
            if (currentShotIndex >= shotAmount)
            {
                _BurstFireRunning = false;
                SpawnBullet();
                continue;
            }
            SpawnBullet();
            currentShotIndex++;
            
            float timer = 0;
            while (timer < delay)
            { 
                yield return null; 
                timer += Time.deltaTime;
            }
        }*/
    }
    
    IEnumerator TimerCoroutine()
    {
        _CanShoot = false;
        float timer = 0;
        while (timer < _GunScript.ShotDelay)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        timer = 0;
        _CanShoot = true;
    }

    IEnumerator ReloadCoroutine()
    {
        _ReloadingAnimator.CrossFade("Reloading", 0, 0);
        _ReloadingAnimator.speed = 1 / _GunScript.ReloadTime;
        _UIAmmo.text = "Reloading";
        _CanShoot = false;
        float timer = 0;
        while (timer < _GunScript.ReloadTime)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        _CanShoot = true;
        _GunScript.CurrentMagasine = _GunScript.MagasineSize;
        UpdateUI();
    }

    IEnumerator LookingDownSightCoroutine()
    {
        _CanShoot = false;
        _LookingDownSight = true;
        Animator animator = Gun.GetComponent<Animator>();
        animator.SetBool("AimingDownSight", true);
        float fieldOfViewGoal = 60 / 2;
        float normalizedTime = -1;
        do
        {
            yield return null;
            float animatorTime = animator.GetAnimatorTransitionInfo(0).normalizedTime;
            if (animatorTime == 0)
            {
                if (normalizedTime == 0)
                {
                    Camera.main.fieldOfView = fieldOfViewGoal;
                    break;
                }

                normalizedTime = 0;
                continue;
            }
            
            Camera.main.fieldOfView = 60 - (60 - fieldOfViewGoal) * animatorTime;
            normalizedTime = animatorTime;
        } while (animator.GetAnimatorTransitionInfo(0).normalizedTime < 1.0f);

        _CanShoot = true;
    }
    
    /*IEnumerator StopLookingDownSightCoroutine()
    {
        _CanShoot = false;
        _LookingDownSight = true;
        Animator animator = Gun.GetComponent<Animator>();
        animator.SetBool("AimingDownSight", false);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        _CanShoot = true;
    }*/
}

