using System;
using FPS.Scripts.Entities;

namespace FPS.Scripts.Interfaces
{
    public interface IBullet
    {
        public Player Owner { get; set; }
        public Action<int, Player> OnDealingDamage { get; set; }
    }
}
