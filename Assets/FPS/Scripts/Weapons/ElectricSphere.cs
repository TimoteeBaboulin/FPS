using System;
using System.Collections.Generic;
using System.Linq;
using FPS.Scripts.Entities;
using FPS.Scripts.System;
using FPS.Scripts.Weapons.Bullets.Interface;
using UnityEngine;

namespace FPS.Scripts.Weapons
{
    public class ElectricSphere : TickDependant, ISpawnManager
    {
        [SerializeField] private float _Range;
        [SerializeField] private int _TickDamage;
        [SerializeField] private GameObject _ElectricArcVfx;
        private Dictionary<Entity, GameObject> _ElectricArcs = new Dictionary<Entity, GameObject>();
        private int _TicksLifeTime = 90;

        public Player Owner { get; set; }

        public override void OnTick()
        {
            if (Owner == null) throw new Exception("Owner not set");
        
            _TicksLifeTime--;
        
            Collider[] colliders = Physics.OverlapSphere(transform.position, _Range);
            List<Entity> entities = new List<Entity>();

            foreach (var collider in colliders)
            {
                Entity currentEntity = collider.GetComponentInParent<Entity>();
                if (currentEntity == null || entities.Contains(currentEntity)) continue;
            
                currentEntity.TakeDamage(_TickDamage, "Electric Sphere");
                //ScoreboardUI.Instance.UpdatePlayerDamage(_TickDamage, Owner);
                entities.Add(currentEntity);
            }

            //Get every entity that's in the old table and no longer in the new one
            var temporaryList = _ElectricArcs.Keys.ToList().Except(entities);
        
            //destroy them
            if (temporaryList != null) { foreach (var entity in temporaryList) Destroy(_ElectricArcs[entity]); }

            temporaryList = entities.Except(_ElectricArcs.Keys.ToList());

            if (temporaryList != null) {
                foreach (var entity in temporaryList) {
                    GameObject currentArc = Instantiate(_ElectricArcVfx, transform);
                    currentArc.GetComponent<ElectricArcHandler>().SetUp(transform, entity.transform);
                    _ElectricArcs.Add(entity, currentArc);
                }
            }

            if (_TicksLifeTime <= 0)
            {
            
                Destroy(gameObject);
            }
        }
    }
}
