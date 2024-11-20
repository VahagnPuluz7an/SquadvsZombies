using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Squad.UI
{
    public class BrawlerSpawnUI : MonoBehaviour
    {
        public static event Action<int> OnPointerUp;
        public static event Action<int> OnPointerDrag;
        public static event Action<int> OnPointerDown;

        [SerializeField] private int buttonIndex;
        
        public void OnUp(BaseEventData eventData)
        {
            OnPointerUp?.Invoke(buttonIndex);
        }
        
        public void OnDrag(BaseEventData eventData)
        {
            OnPointerDrag?.Invoke(buttonIndex);
        }

        public void OnDown(BaseEventData eventData)
        {
            OnPointerDown?.Invoke(buttonIndex);
        }
    }
}