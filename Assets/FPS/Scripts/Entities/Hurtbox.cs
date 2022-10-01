using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class Hurtbox : MonoBehaviour, IDamageable
{
    private Entity _Master;
    public GameObject BodyPart = null;
    [SerializeField] private float _DamageModifier = 1;

    private void Awake()
    {
        _Master = GetComponentInParent<Entity>();
    }

    private void Update()
    {
        if (BodyPart == null || !BodyPart.transform.hasChanged) return;

        transform.position = BodyPart.transform.position;
        transform.rotation = BodyPart.transform.rotation;
    }

    public int GetHit(int damage)
    {
        if (_Master == null)
            throw new NullReferenceException("Master was not set.");
        
        _Master.TakeDamage((int) (damage * _DamageModifier), gameObject.name);
        return (int) (damage * _DamageModifier);
    }

    private void OnDestroy()
    {
        if (GetComponent<TrailRenderer>() == null) return;

        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        GameObject newObject = Instantiate<GameObject>(new GameObject());
        newObject.AddComponent<TrailRenderer>();
    }
}
