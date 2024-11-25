using System;
using UnityEngine;

namespace Squad
{
    [CreateAssetMenu(fileName = "BrawlersScriptable", menuName = "ScriptableObjects/BrawlersScriptable")]
    public class BrawlersScriptable : ScriptableObject
    {
        [field: SerializeField] public BrawlerBuyData[] Brawlers { get; private set; }
    }

    [Serializable]
    public struct BrawlerBuyData
    {
        public GameObject Brawler;
        public int Price;
    }
}