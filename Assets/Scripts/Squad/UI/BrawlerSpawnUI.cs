using System;
using Coins;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Squad.UI
{
    public class BrawlerSpawnUI : MonoBehaviour
    {
        public static event Action<int> Bought;
        public static event Action<int> OnPointerUp;
        public static event Action<int> OnPointerDrag;
        public static event Action<int> OnPointerDown;

        [SerializeField] private int buttonIndex;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Button button;

        [Inject] private BrawlersScriptable _brawlersScriptable;
        [Inject] private CoinsCounter _coinsCounter;
        
        public void OnUp(BaseEventData eventData)
        {
            if (!button.interactable) return;
            OnPointerUp?.Invoke(buttonIndex);
            Bought?.Invoke(_brawlersScriptable.Brawlers[buttonIndex].Price);
        }
        
        public void OnDrag(BaseEventData eventData)
        {
            if (!button.interactable) return;
            OnPointerDrag?.Invoke(buttonIndex);
        }

        public void OnDown(BaseEventData eventData)
        {
            if (!button.interactable) return;
            OnPointerDown?.Invoke(buttonIndex);
        }

        private void Start()
        {
            UpdatePriceText(_coinsCounter.CoinCount);
            _coinsCounter.CountUpdated += UpdatePriceText;
        }

        private void OnDestroy()
        {
            _coinsCounter.CountUpdated -= UpdatePriceText;
        }

        private void UpdatePriceText(int playerCoinCount)
        {
            int price = _brawlersScriptable.Brawlers[buttonIndex].Price;
            priceText.SetText(price.ToString());
            button.interactable = price <= playerCoinCount;
        }
    }
}