using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Shoot : MonoBehaviour
{
    [SerializeField] private Gun _GunScript;
    [SerializeField] private Text _UIAmmo;
    [SerializeField] private Animator _ReloadingAnimator;
    [SerializeField] private Transform _GunBarrel;
    public GameObject Gun;
    private bool _BurstFireRunning = false;
    /*private GunState _State;*/
    
    private bool _CanShoot = true;
    
    // Start is called before the first frame update
    void Start()
    {
        /*_State = GunState.IDLE;*/
        UpdateUI();
        _CanShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Si tu ne petu pas tirer, ou que tu n'appuies sur aucun bouton
        if (!_CanShoot || (!Input.GetButton("Fire1") && !Input.GetButton("Reloading") && !Input.GetButton("Fire2")))
            return;
        
        if (_GunScript.MagasineSize > 0 && _GunScript.CurrentMagasine < _GunScript.MagasineSize &&
            Input.GetButton("Reloading"))
        {
            StartCoroutine(ReloadCoroutine());
            return;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            GetComponentInChildren<Animator>().SetBool("AimingDownSight", true);
        }
        
        if (_GunScript.CurrentMagasine <= 0 && _GunScript.MagasineSize > 0) {
            StartCoroutine(ReloadCoroutine());
            return;
        }
        
        Fire();

    }

    private bool Fire()
    {
        
        switch (_GunScript.GunType)
        {
            case GunTypeEnum.Auto:
                SpawnBullet();
                break;
            
            case GunTypeEnum.SemiAuto:
                if (!Input.GetButtonDown("Fire1")) return false;
                SpawnBullet();
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
                //Spawn the physical projectile
                GameObject shot = Instantiate(_GunScript.Projectile, transform.position + transform.forward * _GunScript.BulletDistance, transform.rotation);
                //Spawn the visual projectile, mostly a trail
                GameObject visualProjectile = Instantiate(_GunScript.VisualProjectile, _GunBarrel.position, transform.rotation);

                //Proceed to shoot a ray to find where the bullet should reach
                RaycastHit hit = new RaycastHit();
                Physics.Raycast(transform.position, transform.forward, out hit, 100, LayerMask.GetMask("Hitbox", "Terrain"));
                //If we didn't find a target, just shoot towards the middle
                if (hit.collider == null)
                    visualProjectile.transform.LookAt(transform.position + transform.forward * 100);
                //If we did find, shoot toward the point of the hitbox that was hit
                else
                    visualProjectile.transform.LookAt(hit.point);
                
                //Set the shot's owner to be the player
                shot.GetComponent<INewBullet>().Owner = GetComponentInParent<Player>();
                break;
            
            case ShotTypeEnum.ShotShell:
                SpawnPellets(10, 8);
                break;
        }
        
        Gun.GetComponent<Animator>().CrossFade("Gun Animation", 0);
        if (_BurstFireRunning) return;
        _GunScript.CurrentMagasine--;
        UpdateUI();
        StartCoroutine(TimerCoroutine());
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
            
            //Generate the bullet and set-up it's base stats
            GameObject shot = Instantiate(_GunScript.Projectile, transform.position + transform.forward * _GunScript.BulletDistance, transform.rotation);
            shot.GetComponent<INewBullet>().Owner = GetComponentInParent<Player>();

            //Add the generated bullets to x and y axis to generate a random pattern
            Vector3 euler = shot.transform.rotation.eulerAngles;
            euler.x += randomPoint.x;
            euler.y += randomPoint.y;
            shot.transform.rotation = Quaternion.Euler(euler);
            
            //Generate the visual projectile to follow the predicted hits
            GameObject visualProjectile = Instantiate(_GunScript.VisualProjectile, shot.transform.position,
                shot.transform.rotation);
            RaycastHit hit;
            Physics.Raycast(shot.transform.position, shot.transform.forward, out hit, 100, LayerMask.GetMask("Terrain", "Hitbox"));
            if (hit.collider == null)
                visualProjectile.transform.LookAt(shot.transform.position + shot.transform.forward * 100);
            else
                visualProjectile.transform.LookAt(hit.point);
        }
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
        _BurstFireRunning = true;
        int currentShotIndex = 1;

        while (_BurstFireRunning)
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
        }
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
}
