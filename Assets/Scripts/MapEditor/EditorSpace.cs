using System.Collections.Generic;
using System.Linq;
using ReMaz.Grid;
using ReMaz.Grid.Maps;
using ReMaz.Grid.Tiles;
using ReMaz.MapEditor.Tiles;
using ReMaz.MapEditor.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.MapEditor
{
    public class EditorSpace : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<TileDescription> TileToPaint {get; private set; }
        public List<TilePainted> Painted { get; private set; }
        public bool CanPlace => _placeZone.CanPlace;

        private IEditor<Map> _editor;
        
        [SerializeField] private PlaceZone _placeZone;
        [SerializeField] private TileList _tileList;
        [SerializeField] private Image _colorSource;

        private void Awake()
        {
            Painted = new List<TilePainted>();
            
            _editor = FindObjectOfType<MapEditor>();
            
            _editor.ProjectLoaded
                .Subscribe(ProjectLoaded)
                .AddTo(this);
        }

        private void Start()
        {
            TileToPaint = _tileList.Selected.ToReadOnlyReactiveProperty();
        }

        private void ProjectLoaded(Map project)
        {
            foreach (TileSpatial tile in project.Tiles)
            {
                CreateInstance(tile.Position, _tileList.TileDatabase.FindTile(tile.Id));
            }
        }
        
        private void CreateInstance(GridPosition gridPosition, TileDescription tileToPaint)
        {
            GameObject instance = Instantiate(tileToPaint.Prefab, transform);
            instance.transform.position = gridPosition.ToWorld();
            instance.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, tileToPaint.Rotation));

            TilePainted tilePainted = new TilePainted(tileToPaint.Id, instance, gridPosition)
            {
                Graphics = instance.GetComponentInChildren<SpriteRenderer>(),
                Color = _colorSource.color
            };

            tilePainted.Graphics.color = tilePainted.Color;
            
            Painted.Add(tilePainted);
        }

        public bool Paint(GridPosition gridPosition, TileDescription tileToPaint)
        {
            TilePainted existentTile = Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            if (existentTile == null)
            {
                CreateInstance(gridPosition, tileToPaint);

                TileSpatial tileSpatial = new TileSpatial(tileToPaint.Id, gridPosition, TileColor.FromColor(_colorSource.color));
                _editor.Project.Tiles.Add(tileSpatial);

                return true;
            }

            return false;
        }

        public bool Erase(GridPosition gridPosition)
        {
            TilePainted tilePainted = Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
            
            if (tilePainted != null)
            {
                Destroy(tilePainted.Instance);
                Painted.Remove(tilePainted);

                TileSpatial tileSpatial = _editor.Project.Tiles.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
                _editor.Project.Tiles.Remove(tileSpatial);

                return true;
            }

            return false;
        }
    }
}