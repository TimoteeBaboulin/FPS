using FPS.Scripts.Entities;
using FPS.Scripts.Weapons.Bullets.Interface;
using UnityEngine;

namespace FPS.Scripts.Weapons.Bullets.OnHitEffects
{
    public class OnHitDamage : MonoBehaviour
    {
        [SerializeField] private int _BaseDamage;
        [SerializeField] private GameEvent _event;
        private void Start()
        {
            GetComponent<INewBullet>().OnHit += OnHit;
        }

        private void OnHit(Collider collider) {
            if (!collider.GetComponent<Hurtbox>()) return;

            _event.Raise();
            int damageDealt = collider.GetComponent<Hurtbox>().GetHit(_BaseDamage);
            //ScoreboardUI.Instance.UpdatePlayerDamage(damageDealt, GetComponent<INewBullet>().Owner);
        }
    }
}
