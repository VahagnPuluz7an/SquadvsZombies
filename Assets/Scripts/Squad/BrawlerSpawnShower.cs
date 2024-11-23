using System;
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
                _newBrawler = Instantiate(_brawlersScriptable.Brawlers[buttonIndex]);

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                var z = Mathf.RoundToInt(hit.point.z / gridSize.z);
                if (z < 0)
                    z = 0;
                _newBrawler.transform.position =
                    Vector3.Scale(
                        new Vector3(Mathf.RoundToInt(hit.point.x / gridSize.x), 0, z), gridSize);
            }
        }

        private void BrawlerSpawnUIOnOnPointerUp(int buttonIndex)
        {
            if (_newBrawler == null)
                return;
            
            _brawlerPos = _newBrawler.transform.position;
            _brawlerIndex = buttonIndex;
            Attached?.Invoke(_brawlerIndex, _brawlerPos);
            Destroy(_newBrawler);
        }
    }
}