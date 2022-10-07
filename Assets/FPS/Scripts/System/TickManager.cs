using System;
using UnityEngine;

namespace FPS.Scripts.System
{
    public class TickManager : MonoBehaviour
    {
        public static TickManager Instance;
        public Action OnTick;
        public int TicksPerSecond;
        private float _Timer;
        private int _TotalTicks;

        private void Awake()
        {
            _Timer = 0;
            _TotalTicks = 0;

            if (Instance == null) Instance = this;
        }

        void Update() {
            _Timer += Time.deltaTime;
            if (_Timer >= 1 / (float) TicksPerSecond) {
                int ticks = (int) (_Timer / (1 / (float) TicksPerSecond));
                _Timer -= (1 / (float) TicksPerSecond) * ticks;
                _TotalTicks += ticks;
            
                if (OnTick == null) return;
            
                for (int x = 1; x <= ticks; x++) {
                    OnTick.Invoke();
                }
            }
        }
    }
}