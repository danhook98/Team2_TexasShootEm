using UnityEngine;
using UnityEngine.Events;

namespace TexasShootEm.EventSystem
{
    public class AbstractEventListener<T> : MonoBehaviour
    {
        [SerializeField] private AbstractEvent<T> eventToListen;
        [SerializeField] private UnityEvent<T> onEvent;

        protected void OnEnable() => eventToListen.Register(this);
        protected void OnDisable() => eventToListen.Unregister(this);

        public void Listen(T value) => onEvent?.Invoke(value);
    }
}