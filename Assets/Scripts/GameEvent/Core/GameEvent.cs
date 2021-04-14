using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BH.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Event/Game Event Data")]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();


        public void Raise()
        {
            foreach (var eventListener in listeners)
            {
                eventListener.OnCallEvent();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
