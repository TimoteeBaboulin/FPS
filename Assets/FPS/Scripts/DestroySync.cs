using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySync : MonoBehaviour
{
    public GameObject SyncedObject = null;

    private void OnDestroy()
    {
        if (SyncedObject != null) Destroy(SyncedObject);
    }
}
