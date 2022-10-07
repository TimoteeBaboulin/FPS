using System.Collections;
using FPS.Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.Scripts.UI
{
    public class UIEventTest : MonoBehaviour
    {
        public Entity[] TargetEvents;
        private Coroutine _CurrentCoroutine;
        // Start is called before the first frame update
        void Start()
        {
            TargetEvents = GameObject.FindObjectsOfType<Entity>();
            foreach (var entity in TargetEvents)
            {
                entity.SubToDamageEvent(DamagedUpdate);
            }
        }

        void DamagedUpdate(int damage, string hitboxName)
        {
            if (_CurrentCoroutine == null)
                _CurrentCoroutine = StartCoroutine(DamagedUpdateCoroutine(damage, hitboxName));
            else {
                StopCoroutine(_CurrentCoroutine);
                _CurrentCoroutine = StartCoroutine(DamagedUpdateCoroutine(damage, hitboxName));
            }
        }

        IEnumerator DamagedUpdateCoroutine(int damage, string hitboxName)
        {
            float timer = 0;
        
            GetComponent<Text>().text = "Hit " + hitboxName + " for " + damage.ToString() + " damage.";

            while (timer < 2) {
                timer += Time.deltaTime;
                yield return null;
            }

            GetComponent<Text>().text = "No Damage in 2s";
        }
    }
}
