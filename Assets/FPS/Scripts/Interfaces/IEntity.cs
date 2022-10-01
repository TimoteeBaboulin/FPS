using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    Action<int, string> OnDeath { get; set; }
    Action<int, string> OnDamaged { get; set; }

    public void TakeDamage(int damage, string hitboxName);
    public void SubToDeathEvent(Action<int, string> function);
    public void SubToDamageEvent(Action<int, string> function);
}
