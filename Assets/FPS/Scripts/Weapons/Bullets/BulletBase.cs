using System;
using FPS.Scripts.Weapons.Bullets.Interface;
using UnityEngine;

namespace FPS.Scripts.Weapons.Bullets
{
    [Serializable]
    public class BulletBase : MonoBehaviour, INewBullet
    {
        [SerializeField] private float _Speed = 2;
    
        public Player Owner { get; set; }
        public Action<Collider> OnHit { get; set; }

        private void Update()
        {
            if (CheckForCollision()) return;
            Move();
        }

        private bool CheckForCollision()
        {
            RaycastHit hit = new RaycastHit();
            int layerMask = LayerMask.GetMask("Hitbox", "Terrain");
            Physics.Raycast(transform.position, transform.forward, out hit, _Speed * Time.deltaTime, layerMask);
        
            if (hit.collider != null)
            {
                if (OnHit != null)
                    OnHit.Invoke(hit.collider);
                Destroy(gameObject);
            }

            return false;
        }

        private void Move()
        {
            transform.position += transform.forward * _Speed * Time.deltaTime;
        }
    }
}
