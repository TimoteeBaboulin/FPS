using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxBase : MonoBehaviour, IDamageable
{
    private Entity _Master;
    private float _DamageModifier;

    public int GetHit(int damage)
    {
        return 0;
    }
}
