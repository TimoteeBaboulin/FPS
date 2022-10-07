using System;
using UnityEngine;

namespace FPS.Scripts.System
{
    public class FrameRateLock : MonoBehaviour
    {
        public static FrameRateLock Instance;
        public int FPS = 60;
    
        void Start()
        {
            if (Instance != null) throw new Exception("Another FrameRateLock already exists");

            Instance = this;
            Application.targetFrameRate = FPS;
        }
    }
}
