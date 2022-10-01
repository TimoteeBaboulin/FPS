using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDamage : MonoBehaviour
{
    [SerializeField] private int _BaseDamage;
    private void Start()
    {
        GetComponent<INewBullet>().OnHit += OnHit;
    }

    private void OnHit(Collider collider) {
        if (!collider.GetComponent<Hurtbox>()) return;

        int damageDealt = collider.GetComponent<Hurtbox>().GetHit(_BaseDamage);
        ScoreboardUI.Instance.UpdatePlayerDamage(damageDealt, GetComponent<INewBullet>().Owner);
    }
}
