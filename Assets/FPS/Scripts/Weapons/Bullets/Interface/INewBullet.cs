using System;
using UnityEngine;

namespace FPS.Scripts.Weapons.Bullets.Interface
{
    public interface INewBullet
    {
        public Player Owner { get; set; }
        public Action<Collider> OnHit { get; set; }
    }
}
