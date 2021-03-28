using System.Collections.Generic;
using System.Linq;
using Remaz.Game.Grid;
using Remaz.Game.Grid.Tiles;
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
        [SerializeField] private Color _restColor;
        [SerializeField] private Color _overlapColor;

        private TileDescription _tileToPaint;
        private List<TilePlaced> _instances;
        private TilePlaced _selectedInstance;
        private GameObject _ghost;
        private bool _replace;
        
        private void Start()
        {
            _instances = new List<TilePlaced>();
            
            _tileList.Selected
                .Subscribe(tile => _tileToPaint = tile)
                .AddTo(this);

            Camera cam = Camera.main;

            if (cam != null)
            {
                var mouseGridPositionStream = Observable.EveryUpdate()
                    .Select(_ => GridPosition.FromWorld(cam.ScreenToWorldPoint(Input.mousePosition)));

                mouseGridPositionStream
                    .Subscribe(TryDrawGhost)
                    .AddTo(this);

                mouseGridPositionStream
                    .Subscribe(UpdateSelectedInstance)
                    .AddTo(this);

                _tileList.Selected
                    .Where(tile => tile != null)
                    .Subscribe(UpdateGhost)
                    .AddTo(this);
                
                var placeStream = mouseGridPositionStream
                    .Where(_ => _placeZone.CanPlace && _tileToPaint != null);
                
                placeStream
                    .Where(_ => Input.GetMouseButton(0))
                    .Subscribe(TryPlace)
                    .AddTo(this);

                placeStream
                    .Where(_ => Input.GetMouseButton(1))
                    .Subscribe(TryRemove)
                    .AddTo(this);

                Observable.EveryUpdate()
                    .Subscribe(_ => _replace = Input.GetKey(KeyCode.LeftShift))
                    .AddTo(this);
            }
        }

        private void TryPlace(GridPosition gridPosition)
        {
            TilePlaced existentTile = _instances.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
            
            if (existentTile == null)
            {
                GameObject instance = Instantiate(_tileToPaint.Prefab, transform);
                instance.transform.position = gridPosition.ToWorld();

                TilePlaced tilePlaced = new TilePlaced(_tileToPaint.Id, instance, gridPosition);
                _instances.Add(tilePlaced);
                
                TileSpatial tileSpatial = new TileSpatial(_tileToPaint.Id, gridPosition);
                EditorProject.CurrentProject.Tiles.Add(tileSpatial);
            }
            else if(_tileToPaint.Id != existentTile.Id && _replace)
            {
                TryRemove(gridPosition);
                TryPlace(gridPosition);
            }
        }

        private void UpdateSelectedInstance(GridPosition gridPosition)
        {
            TilePlaced tilePlaced = _instances.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            if (tilePlaced == _selectedInstance && tilePlaced != null)
            {
                if (!_replace)
                {
                    SpriteRenderer spriteRenderer = _selectedInstance.Instance.GetComponentInChildren<SpriteRenderer>();
                    spriteRenderer.color = _restColor;
                }
                
                return;
            }
            
            if (_selectedInstance != null)
            {
                SpriteRenderer spriteRenderer = _selectedInstance.Instance.GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.color = _restColor;
            }

            _selectedInstance = tilePlaced;
        }
        
        private void TryRemove(GridPosition gridPosition)
        {
            TilePlaced tilePlaced = _instances.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
            
            if (tilePlaced != null)
            {
                if (tilePlaced == _selectedInstance)
                {
                    _selectedInstance = null;
                }
                
                Destroy(tilePlaced.Instance);
                _instances.Remove(tilePlaced);

                TileSpatial tileSpatial = EditorProject.CurrentProject.Tiles.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
                EditorProject.CurrentProject.Tiles.Remove(tileSpatial);
            }
        }

        private void UpdateGhost(TileDescription tileDescription)
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
        
        private void TryDrawGhost(GridPosition gridPosition)
        {
            if (_ghost != null)
            {
                if (_placeZone.CanPlace)
                {
                    if (_replace)
                    {
                        if (_selectedInstance != null && _selectedInstance.Id != _tileToPaint.Id)
                        {
                            SpriteRenderer spriteRenderer =
                                _selectedInstance.Instance.GetComponentInChildren<SpriteRenderer>();
                            spriteRenderer.color = _overlapColor;
                        }

                        _ghost.transform.position = gridPosition.ToWorld();
                    }
                    else if(!_instances.Any(tile => tile.Position.Overlap(gridPosition)))
                    {
                        _ghost.transform.position = gridPosition.ToWorld();
                    }
                    else
                    {
                        _ghost.transform.position = Vector3.one * 100000f;
                    }
                }
                else
                {
                    _ghost.transform.position = Vector3.one * 100000f;
                }
            }
        }
    }
}