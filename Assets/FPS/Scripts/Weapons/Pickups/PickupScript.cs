using System;
using UnityEngine;

namespace FPS.Scripts.Weapons.Pickups
{
    public class PickupScript : MonoBehaviour
    {
        public GameObject PickupGun;

        private void Awake()
        {
            if (PickupGun.GetComponent<Gun>() == null) return;
            GetComponentInChildren<MeshFilter>().mesh = PickupGun.GetComponentInChildren<MeshFilter>().sharedMesh;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Player player = other.GetComponent<Player>();

            player.Gun = PickupGun;
        }
    }
}
