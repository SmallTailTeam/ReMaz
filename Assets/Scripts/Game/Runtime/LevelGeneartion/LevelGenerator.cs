using ReMaz.Core.Grid;
using ReMaz.Core.Grid.Tiles;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace Game.Runtime.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField, AutoHook] private LevelMovement _levelMovement;
        [SerializeField] private TileDatabase _tileDatabase;

        private Pattern _pattern;
        private int _unitsMoved = 1;
        private int _length;

        private void Start()
        {
            _levelMovement.MovedUnit
                .Subscribe(_ => Tick())
                .AddTo(this);
        }

        private void Tick()
        {
            // Reset
            if (_unitsMoved > _length)
            {
                Project project = ProjectManager.Projects[Random.Range(0, ProjectManager.Projects.Count)];
                _pattern = project.Pattern;
                _unitsMoved = 0;
                _length = _pattern.BoundRight - _pattern.BoundLeft;
            }

            // Instantiate
            foreach (TileSpatial tileSpatial in _pattern.Tiles)
            {
                if (_unitsMoved != tileSpatial.Position.x - _pattern.BoundLeft)
                {
                    continue;
                }

                GameObject prefab = _tileDatabase.FindTile(tileSpatial.Id).Prefab;
                GameObject instance = Instantiate(prefab, transform);
                
                Vector3 position = tileSpatial.Position.ToWorld();
                position.x = ScreenGrid.Size.Value.x + 1f;
                
                instance.transform.position = position;
            }
            
            // Move through
            _unitsMoved++;
            
            Debug.Log(_unitsMoved);
        }
    }
}