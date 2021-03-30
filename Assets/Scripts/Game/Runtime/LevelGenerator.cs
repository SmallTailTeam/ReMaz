using Remaz.Core.Grid;
using ReMaz.Core.Grid;
using Remaz.Core.Grid.Tiles;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace Game.Runtime
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField, AutoHook] private LevelMovement _levelMovement;
        [SerializeField] private TileDatabase _tileDatabase;

        private Pattern _currentPattern;
        private int _unitsMoved;
        
        private void Start()
        {
            _levelMovement.MovedUnit
                .Subscribe(_ => Spawn())
                .AddTo(this);
        }

        private void Spawn()
        {
            if (_currentPattern != null && _unitsMoved < _currentPattern.BoundRight - _currentPattern.BoundLeft)
            {
                _unitsMoved++;
                return;
            }
            
            Project project = ProjectManager.Projects[Random.Range(0, ProjectManager.Projects.Count)];
            _currentPattern = project.Pattern;
            
            foreach (TileSpatial tile in _currentPattern.Tiles)
            {
                TileDescription tileDescription = _tileDatabase.FindTile(tile.Id);

                GameObject instance = Instantiate(tileDescription.Prefab, transform);

                GridPosition position = tile.Position;
                position.x -= _currentPattern.BoundLeft;
                instance.transform.position = position.ToWorld() + new Vector3(ScreenGrid.Size.Value.x, 0f, 0f);
            }

            _unitsMoved = 0;
        }
    }
}