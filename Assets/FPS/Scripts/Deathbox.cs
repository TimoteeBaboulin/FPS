using UnityEngine;

namespace FPS.Scripts
{
    public class Deathbox : MonoBehaviour
    {
        void OnTriggerExit(Collider collider) { Destroy(collider.gameObject); }
    }
}
