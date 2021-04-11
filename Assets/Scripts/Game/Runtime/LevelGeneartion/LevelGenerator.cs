using ReMaz.Core.Content;
using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Patterns;
using ReMaz.Core.Content.Projects.Tiles;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Game.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField, AutoHook] private LevelMovement _levelMovement;
        [SerializeField, AutoHook] private TilePool _tilePool;
        [SerializeField] private TileDatabase _tileDatabase;

        private IContentContainer<ProjectPattern> _projectList;
        
        private Pattern _pattern;
        private int _unitsMoved = 1;
        private int _length;

        private void Awake()
        {
            _projectList = FindObjectOfType<PatternList>();
        }

        private void Start()
        {
            _levelMovement.MovedUnit
                .Subscribe(Tick)
                .AddTo(this);
        }
        
        private void Tick(float compensation)
        {
            // Reset
            if (_unitsMoved > _length)
            {
                ProjectPattern projectPattern = _projectList.GetRandom();
                _pattern = projectPattern.Content;
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

                TileDescription tileDescription = _tileDatabase.FindTile(tileSpatial.Id);
                
                GameObject instance = _tilePool.GetInstance(tileDescription.Prefab);
                
                Vector3 position = tileSpatial.Position.ToWorld();
                position.x = ScreenGrid.Size.Value.x * 0.5f + 1f ;
                instance.transform.position = position;
                instance.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tileDescription.Rotation));
            }
            
            // Move through
            _unitsMoved++;
        }
    }
}