using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ElectricArcHandler : MonoBehaviour
{
    private Transform _Origin = null;
    private Transform _Goal = null;

    private void Awake()
    {
        _Origin = _Goal = transform;
    }

    public void SetUp(Transform origin, Transform goal) {
        _Origin = origin;
        _Goal = goal;
    }

    private void Update()
    {
        if (_Goal == null || _Origin == null) return;
        if (!_Goal.hasChanged && !_Origin.hasChanged) return;

        //Set first child's local position to be the same as the origin
        transform.GetChild(0).position = _Origin.position;
        

        Vector3 originPosition = _Origin.position;
        Vector3 goalPosition = _Goal.position + _Goal.up * _Goal.localScale.z;

        Vector3 originToGoalVector = new Vector3(goalPosition.x - originPosition.x,
            goalPosition.y - originPosition.y,
            goalPosition.z - originPosition.z);

        transform.GetChild(1).position =_Origin.position + originToGoalVector * (1f / 3f);
        transform.GetChild(1).position =_Origin.position + originToGoalVector * (2f / 3f);
        //Set the final child this time
        transform.GetChild(3).position = goalPosition;
    }
}
