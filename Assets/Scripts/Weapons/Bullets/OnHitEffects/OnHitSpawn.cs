using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _Prefab;
    [SerializeField] private bool _StickToTarget = true;
    [SerializeField] private Vector3 _Offset = Vector3.zero;

    private void Awake()
    {
        GetComponent<INewBullet>().OnHit += OnHit;
    }
    
    void OnHit(Collider collider)
    {
        if (_Prefab == null || !collider.GetComponent<Hurtbox>()) return;

        GameObject newItem = Instantiate(_Prefab, collider.transform);
        newItem.transform.position = collider.ClosestPoint(transform.position) + _Offset;

        newItem.GetComponent<ISpawnManager>().Owner = GetComponent<INewBullet>().Owner;
    }
}
