using System.Collections.Generic;
using UnityEngine;

namespace TexasShootEm.EventSystem
{
    public class AbstractEvent<T> : ScriptableObject
    {
        private List<AbstractEventListener<T>> _listeners = new();

        public void Register(AbstractEventListener<T> listener)
        {
            if (!_listeners.Contains(listener)) { _listeners.Add(listener); }
        }

        public void Unregister(AbstractEventListener<T> listener)
        {
            if (_listeners.Contains(listener)) { _listeners.Remove(listener); }
        }

        public void Invoke(T value)
        {
            foreach (var listener in _listeners) { listener.Listen(value); }
        }
    }
    
    public readonly struct Empty {}
}
