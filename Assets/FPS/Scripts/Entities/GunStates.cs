using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunIdleState : State
{
    public GunIdleState() : base()
    {
        state = GunState.IDLE;
    }
    
    public void Enter()
    {
        base.Enter();
    }
}
