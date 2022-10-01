using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public Gun PickupGun;

    private void Awake()
    {
        if (PickupGun.GunMesh == null) return;
        GetComponentInChildren<MeshFilter>().mesh = PickupGun.GunMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Shoot shootingScript = other.GetComponentInChildren<Shoot>();
        
        if (shootingScript == null) throw new Exception("Shoot Scrip not found");

        shootingScript.ChangeGun(PickupGun);
    }
}
