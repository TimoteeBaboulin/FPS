using UnityEngine;

namespace FPS.Scripts
{
    public class DestroySync : MonoBehaviour
    {
        public GameObject SyncedObject = null;

        private void OnDestroy()
        {
            if (SyncedObject != null) Destroy(SyncedObject);
        }
    }
}
