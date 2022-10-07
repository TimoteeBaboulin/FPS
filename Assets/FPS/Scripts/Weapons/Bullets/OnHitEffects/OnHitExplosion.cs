using System.Collections.Generic;
using FPS.Scripts.Entities;
using FPS.Scripts.Weapons.Bullets.Interface;
using UnityEngine;

namespace FPS.Scripts.Weapons.Bullets.OnHitEffects
{
    public class OnHitExplosion : MonoBehaviour
    {
        [SerializeField] private float _Radius = 1;
        [SerializeField] private int _BaseDamage = 50;

        private void Awake()
        {
            GetComponent<INewBullet>().OnHit += OnHit;
        }

        private void OnHit(Collider collider)
        {
            //Get all colliders hit by the explosion's radius, and initialize a List to hold every unique instance of target
            Collider[] collidersHit = Physics.OverlapSphere(collider.ClosestPoint(transform.position), _Radius);
            List<Entity> uniqueTargets = new List<Entity>();

            //for each collider, verify if it is a hitbox and if the target's already in the list
            //in the case it is the hitbox of a target that's still not in the list, just add it to the list
            foreach (var hit in collidersHit)
            {
                if (!hit.GetComponent<Hurtbox>()) continue;

                if (uniqueTargets.Contains(hit.GetComponentInParent<Entity>())) continue;
            
                uniqueTargets.Add(hit.GetComponentInParent<Entity>());
            }

            //verify if you actually have any target to run the code on
            if (uniqueTargets.Count == 0)
                return;
        
            //reduce every target's health by the explosion damage
            foreach (var target in uniqueTargets)
            {
                target.TakeDamage(_BaseDamage, "Explosion");
            }
            //update the scoreboard accordingly
            //ScoreboardUI.Instance.UpdatePlayerDamage(_BaseDamage * uniqueTargets.Count, GetComponent<INewBullet>().Owner);
        }
    }
}
