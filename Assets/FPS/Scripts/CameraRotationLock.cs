using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationLock : MonoBehaviour
{
    [SerializeField] private float _MinimumRotation = 60;
    [SerializeField] private float _MaximumRotation = 60;

    // Update is called once per frame
    void Update()
    {
        if (!transform.hasChanged) return;

        float XRotation = transform.localEulerAngles.x;
        if (XRotation > 180)
            XRotation = Mathf.Clamp(XRotation, 360 - _MaximumRotation, 360);
        if (XRotation < 180)
            XRotation = Mathf.Clamp(XRotation, 0, _MinimumRotation);
        transform.localEulerAngles = new Vector3(XRotation, 0, 0);
        return;
    }
}
