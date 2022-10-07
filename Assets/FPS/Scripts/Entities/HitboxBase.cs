using FPS.Scripts.Interfaces;
using UnityEngine;

namespace FPS.Scripts.Entities
{
    public class HitboxBase : MonoBehaviour, IDamageable
    {
        private Entity _Master;
        private float _DamageModifier;

        public int GetHit(int damage)
        {
            return 0;
        }
    }
}
