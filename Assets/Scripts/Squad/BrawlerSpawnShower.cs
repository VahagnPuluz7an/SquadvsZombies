using System;
using System.Collections.Generic;
using Squad.UI;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Squad
{
    public class BrawlerSpawnShower : MonoBehaviour
    {
        public static event Action<int, float3> Attached;
        
        [SerializeField] private Vector3 gridSize;
        
        private GameObject _newBrawler;
        private int _brawlerIndex;
        private float3 _brawlerPos;
        private int _layerMask;
        private List<Vector3> _spawnedPoses = new();

        [Inject] private BrawlersScriptable _brawlersScriptable;
        
        private void Awake()
        {
            BrawlerSpawnUI.OnPointerUp += BrawlerSpawnUIOnOnPointerUp;
            BrawlerSpawnUI.OnPointerDrag += BrawlerSpawnUIOnOnPointerDrag;
        }

        private void Start()
        {
            _layerMask = LayerMask.GetMask("Platform");
        }

        private void OnDestroy()
        {
            BrawlerSpawnUI.OnPointerUp -= BrawlerSpawnUIOnOnPointerUp;
            BrawlerSpawnUI.OnPointerDrag -= BrawlerSpawnUIOnOnPointerDrag;
        }
        
        private void BrawlerSpawnUIOnOnPointerDrag(int buttonIndex)
        {
            if (_newBrawler == null)
                _newBrawler = Instantiate(_brawlersScriptable.Brawlers[buttonIndex].Brawler);

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (!Physics.Raycast(ray, out var hit, float.MaxValue, _layerMask)) return;
            
            int z = Mathf.RoundToInt(hit.point.z / gridSize.z);
            if (z < 0) z = 0;
            
            var spawnPos = Vector3.Scale(
                new Vector3(Mathf.RoundToInt(hit.point.x / gridSize.x), 0, z), gridSize);

            if (_spawnedPoses.Contains(spawnPos)) spawnPos.z += gridSize.z;
                
            _newBrawler.transform.position = spawnPos;
        }

        private void BrawlerSpawnUIOnOnPointerUp(int buttonIndex)
        {
            if (_newBrawler == null)
                return;
            
            _brawlerPos = _newBrawler.transform.position;
            _brawlerIndex = buttonIndex;
            Attached?.Invoke(_brawlerIndex, _brawlerPos);
            _spawnedPoses.Add(_brawlerPos);
            Destroy(_newBrawler);
        }
    }
}