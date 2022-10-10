using System;
using UnityEngine;

namespace FPS.Scripts.Weapons.Pickups
{
    public class PickupScript : MonoBehaviour
    {
        public GameObject PickupGun;
        public float Scale = 10;
        //public Vector3 Position;

        private void Awake()
        {
            if (PickupGun.GetComponent<Gun>() == null) return;
            GetComponentInChildren<MeshFilter>().mesh = PickupGun.GetComponent<Gun>().CurrentGun.SimplifiedMesh;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Player player = other.GetComponent<Player>();

            player.ChangeWeapon(PickupGun);
        }
    }
}
