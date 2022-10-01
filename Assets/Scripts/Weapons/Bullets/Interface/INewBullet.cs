using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INewBullet
{
    public Player Owner { get; set; }
    public Action<Collider> OnHit { get; set; }
}
