using GD.Types;
using System.Collections.Generic;
using UnityEngine;

//Code by Niall McGuinness

namespace GD.Events
{
    /// <summary>
    /// Implements Observer pattern to enable us to notify multiple objects about
    /// any events that happen to the GameEvent they�re observing.
    /// </summary>
    /// <see cref="https://refactoring.guru/design-patterns/observer"/>
    [CreateAssetMenu(fileName = "GameEvent",
        menuName = "GD/Events/No Params/GameEvent",
        order = 1)]
    public class GameEvent : ScriptableGameObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        [ContextMenu("Raise Event")]
        public virtual void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}