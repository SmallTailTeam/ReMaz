using Remaz.Game.Map;
using ReMaz.PatternEditor.UI;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorSpace : MonoBehaviour
    {
        [SerializeField] private PlaceZone _placeZone;
        [SerializeField] private TileList _tileList;

        private TileDescription _selectedTile;
        
        private void Start()
        {
            _tileList.Selected
                .Subscribe(tile => _selectedTile = tile)
                .AddTo(this);
            
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0) && _placeZone.CanPlace)
                .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition))
                .Subscribe(pos =>
                {
                    GridPosition gridPosition = GridPosition.FromWorld(pos);
                    Debug.Log(gridPosition);
                    Instantiate(_selectedTile.Prefab, transform).transform.position = gridPosition.ToWorld();
                })
                .AddTo(this);
        }
    }
}