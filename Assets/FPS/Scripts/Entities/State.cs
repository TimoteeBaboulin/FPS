using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunState
{
    IDLE,
    AIMING,
    FIRING
};

public class State
{
    public enum Stage
    {
        ENTER,
        UPDATE,
        EXIT
    };

    public GunState state;
    private Stage stage;

    public void Enter() { stage = Stage.UPDATE; }
    public void Update() { stage = Stage.UPDATE; }
    public void Exit() { stage = Stage.EXIT; }
    
    public State()
    {
        stage = Stage.ENTER;
    }

    public State Process() {
        switch (stage)
        {
            case Stage.ENTER:
                Enter();
                break;
            case Stage.UPDATE:
                Update();
                break;
            case Stage.EXIT:
                Exit();
                return this;
        }
        return this;
    }
}
