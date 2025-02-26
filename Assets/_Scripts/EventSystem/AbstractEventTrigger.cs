using UnityEngine;

namespace TexasShootEm.EventSystem
{
    public class AbstractEventTrigger<T> : MonoBehaviour
    {
        [SerializeField] private AbstractEvent<T> eventToTrigger;
        
        public void Trigger(T value) => eventToTrigger.Invoke(value);
    }
}