using TMPro;
using UnityEngine;
using Zenject;

namespace Coins
{
    public class CoinsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text passiveIncomeText;

        [Inject] private CoinsCounter _counter;
        
        private void Start()
        {
            _counter.CountUpdated += UpdateCoinText;
            _counter.PassiveIncomeCountUpdated += UpdatePassiveIncome;
            UpdateCoinText(_counter.CoinCount);
            UpdatePassiveIncome(_counter.PassiveIncome);
        }

        private void OnDestroy()
        {
            _counter.CountUpdated -= UpdateCoinText;
            _counter.PassiveIncomeCountUpdated -= UpdatePassiveIncome;
        }

        private void UpdateCoinText(int coinCount)
        {
            text.SetText(coinCount.ToString());
        }

        private void UpdatePassiveIncome(int passiveIncomeCount)
        {
            passiveIncomeText.SetText(passiveIncomeCount + "/S");
        }
    }
}