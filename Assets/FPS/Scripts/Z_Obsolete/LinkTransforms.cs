using UnityEngine;

namespace FPS.Scripts
{
    [ExecuteInEditMode]
    public class LinkTransforms : MonoBehaviour
    {
        public GameObject Master = null;

        // Update is called once per frame
        void Update()
        {
            if (Master == null || !Master.transform.hasChanged) return;

            transform.position = Master.transform.position;
            transform.rotation = Master.transform.rotation;
        }
    }
}
