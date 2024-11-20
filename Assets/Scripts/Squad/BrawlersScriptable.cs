using UnityEngine;

namespace Squad
{
    [CreateAssetMenu(fileName = "BrawlersScriptable", menuName = "ScriptableObjects/BrawlersScriptable")]
    public class BrawlersScriptable : ScriptableObject
    {
        [field: SerializeField] public GameObject[] Brawlers { get; private set; }
    }
}