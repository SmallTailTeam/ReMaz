using System.Collections.Generic;
using System.Linq;
using Remaz.Game.Map;
using Remaz.Game.Map.Tiles;
using ReMaz.PatternEditor.Tiles;
using ReMaz.PatternEditor.UI;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorSpace : MonoBehaviour
    {
        [SerializeField] private PlaceZone _placeZone;
        [SerializeField] private TileList _tileList;

        private ReactiveProperty<TileDescription> _selectedTile;
        private List<TilePlaced> _instances;
        private GameObject _ghost;
        
        private void Start()
        {
            _selectedTile = new ReactiveProperty<TileDescription>();
            _instances = new List<TilePlaced>();
            
            _tileList.Selected
                .Subscribe(tile => _selectedTile.Value = tile)
                .AddTo(this);

            Camera cam = Camera.main;

            if (cam != null)
            {
                var mouseWorldPosStream = Observable.EveryUpdate()
                    .Select(_ => cam.ScreenToWorldPoint(Input.mousePosition));

                var placeStream = mouseWorldPosStream
                    .Where(_ => _placeZone.CanPlace && _selectedTile != null);
                
                placeStream
                    .Where(_ => Input.GetMouseButton(0))
                    .Subscribe(TryPlace)
                    .AddTo(this);

                placeStream
                    .Where(_ => Input.GetMouseButton(1))
                    .Subscribe(TryRemove)
                    .AddTo(this);

                mouseWorldPosStream
                    .Subscribe(TryDrawGhost)
                    .AddTo(this);

                _selectedTile
                    .Where(tile => tile != null)
                    .Subscribe(SelectedTileChanged)
                    .AddTo(this);
            }
        }

        private void TryPlace(Vector3 position)
        {
            GridPosition gridPosition = GridPosition.FromWorld(position);
            
            if (!_instances.Any(tile => tile.Position.Overlap(gridPosition)))
            {
                GameObject instance = Instantiate(_selectedTile.Value.Prefab, transform);
                instance.transform.position = gridPosition.ToWorld();

                TilePlaced tilePlaced = new TilePlaced(instance, gridPosition);
                _instances.Add(tilePlaced);
                
                TileSpatial tileSpatial = new TileSpatial(_selectedTile.Value.Id, gridPosition);
                EditorProject.CurrentProject.Tiles.Add(tileSpatial);
            }
        }
        
        private void TryRemove(Vector3 position)
        {
            GridPosition gridPosition = GridPosition.FromWorld(position);

            TilePlaced tilePlaced = _instances.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
            
            if (tilePlaced != null)
            {
                Destroy(tilePlaced.Instance);
                _instances.Remove(tilePlaced);

                TileSpatial tileSpatial = EditorProject.CurrentProject.Tiles.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
                EditorProject.CurrentProject.Tiles.Remove(tileSpatial);
            }
        }

        private void SelectedTileChanged(TileDescription tileDescription)
        {
            if (_ghost != null)
            {
                Destroy(_ghost);
            }

            _ghost = Instantiate(tileDescription.Prefab);

            SpriteRenderer spriteRenderer = _ghost.GetComponentInChildren<SpriteRenderer>();
            
            Color color = spriteRenderer.color;
            color.a = 0.3f;
            spriteRenderer.color = color;
        }
        
        private void TryDrawGhost(Vector3 position)
        {
            GridPosition gridPosition = GridPosition.FromWorld(position);
            
            if (_ghost != null)
            {
                if (!_instances.Any(tile => tile.Position.Overlap(gridPosition)) && _placeZone.CanPlace)
                {
                    _ghost.transform.position = gridPosition.ToWorld();
                }
                else
                {
                    _ghost.transform.position = Vector3.one * 1000000f;
                }
            }
        }
    }
}