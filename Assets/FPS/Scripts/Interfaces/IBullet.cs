using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public Player Owner { get; set; }
    public Action<int, Player> OnDealingDamage { get; set; }
}
