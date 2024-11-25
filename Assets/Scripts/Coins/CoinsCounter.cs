using System;
using Enemy;
using Squad.UI;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Coins
{
    public class CoinsCounter : IInitializable, IDisposable, ITickable
    {
        public event Action<int> CountUpdated;
        public event Action<int> PassiveIncomeCountUpdated;
        
        private const string CoinCountKey = "CoinCount";
        private const string PassiveIncomeCountKey = "PassiveIncomeCount";

        public int CoinCount => _coinCount;
        public int PassiveIncome => _passiveIncome;
        
        private int _coinCount;
        private int _passiveIncome;
        private float _passiveIncomeTimer;
        
        public void Initialize()
        {
            _coinCount = PlayerPrefs.GetInt(CoinCountKey);
            _passiveIncome = PlayerPrefs.GetInt(PassiveIncomeCountKey);
            EnemyDeadSystem.EnemyDead += EnemyDeadSystemOnEnemyDead;
            BrawlerSpawnUI.Bought += RemoveCoins;
        }
        
        public void Tick()
        {
            _passiveIncomeTimer += Time.deltaTime;
            
            if (_passiveIncomeTimer < 1) return;
            
            AddCoins(_passiveIncome);
            _passiveIncomeTimer = 0;
        }
        
        public void Dispose()
        {
            EnemyDeadSystem.EnemyDead -= EnemyDeadSystemOnEnemyDead;
            BrawlerSpawnUI.Bought -= RemoveCoins;
        }
        
        private void EnemyDeadSystemOnEnemyDead(float3 pos)
        {
            AddCoins(1);
        }

        private void AddCoins(int count)
        {
            _coinCount += count;
            UpdateCoinCount();
        }
        
        private void RemoveCoins(int count)
        {
            _coinCount -= count;
            UpdateCoinCount();
        }

        private void UpdateCoinCount()
        {
            PlayerPrefs.SetInt(CoinCountKey, _coinCount);
            CountUpdated?.Invoke(_coinCount);
        }

        private void AddPassiveIncome(int count)
        {
            _passiveIncome += count;
            PlayerPrefs.SetInt(PassiveIncomeCountKey, _passiveIncome);
            PassiveIncomeCountUpdated?.Invoke(_passiveIncome);
        }
    }
}
