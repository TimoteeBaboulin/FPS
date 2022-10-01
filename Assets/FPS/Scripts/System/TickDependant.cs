using System;
using UnityEngine;

public abstract class TickDependant : MonoBehaviour
{
    private void Start()
    {
        if (TickManager.Instance == null)
            throw new Exception("Tick manager instance not initialized");

        TickManager.Instance.OnTick += OnTick;
    }

    private void OnDestroy()
    {
        TickManager.Instance.OnTick -= OnTick;
    }

    public abstract void OnTick();
}