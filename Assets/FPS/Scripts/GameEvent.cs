using System.Collections.Generic;
using UnityEngine;

namespace FPS.Scripts
{
    [CreateAssetMenu(menuName = "GameEvent/NewGameEvent", fileName = "NewGameEvent",order = 51)]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> Listeners = new List<GameEventListener>();

        public void AddListener(GameEventListener listener)
        {
            if (!Listeners.Contains(listener))
                Listeners.Add(listener);
        }

        public void RemoveListener(GameEventListener listener)
        {
            if (Listeners.Contains(listener)) 
                Listeners.Remove(listener);
        }

        public void Raise()
        {
            foreach (var listener in Listeners)
            {
                listener.OnEventRaised();
            }
        }
    }
}
