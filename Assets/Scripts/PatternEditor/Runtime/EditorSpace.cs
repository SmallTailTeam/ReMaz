using System.Collections.Generic;
using System.Linq;
using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Patterns;
using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.PatternEditor.Tiles;
using ReMaz.PatternEditor.UI;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorSpace : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<TileDescription> TileToPaint {get; private set; }
        public List<TilePainted> Painted { get; private set; }
        public bool CanPlace => _placeZone.CanPlace;
        public TileDatabase TileDatabase => _tileList.TileDatabase;

        private IProjectEditor<IProject<Pattern>> _projectEditor;
        
        [SerializeField] private PlaceZone _placeZone;
        [SerializeField] private TileList _tileList;

        private void Awake()
        {
            Painted = new List<TilePainted>();
            
            _projectEditor = FindObjectOfType<PatternProjectEditor>();
            
            _projectEditor.ProjectLoaded
                .Subscribe(ProjectLoaded)
                .AddTo(this);
        }

        private void Start()
        {
            TileToPaint = _tileList.Selected.ToReadOnlyReactiveProperty();
        }

        private void ProjectLoaded(IProject<Pattern> project)
        {
            foreach (TileSpatial tile in project.Content.Tiles)
            {
                CreateInstance(tile.Position, TileDatabase.FindTile(tile.Id));
            }
        }
        
        private void CreateInstance(GridPosition gridPosition, TileDescription tileToPaint)
        {
            GameObject instance = Instantiate(tileToPaint.Prefab, transform);
            instance.transform.position = gridPosition.ToWorld();
            instance.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, tileToPaint.Rotation));

            TilePainted tilePainted = new TilePainted(tileToPaint.Id, instance, gridPosition)
            {
                Graphics = instance.GetComponentInChildren<SpriteRenderer>()
            };
            
            Painted.Add(tilePainted);
        }

        public bool Paint(GridPosition gridPosition, TileDescription tileToPaint)
        {
            TilePainted existentTile = Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            if (existentTile == null)
            {
                CreateInstance(gridPosition, tileToPaint);

                TileSpatial tileSpatial = new TileSpatial(tileToPaint.Id, gridPosition);
                _projectEditor.Project.Content.Tiles.Add(tileSpatial);

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

                TileSpatial tileSpatial = _projectEditor.Project.Content.Tiles.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
                _projectEditor.Project.Content.Tiles.Remove(tileSpatial);

                return true;
            }

            return false;
        }
    }
}