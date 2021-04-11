using System.Linq;
using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.PatternEditor.Inputs;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Tiles
{
    public class GhostTileDrawer : MonoBehaviour
    {
        [SerializeField] private Color _visibleColor;
        [SerializeField] private Color _hiddenColor;
        [SerializeField] private Color _restColor;
        [SerializeField] private Color _overlappedColor;
        
        private EditorInputs _inputs;
        private EditorSpace _editorSpace;
        private TileGhost _ghost;
        private TilePainted _selectedTile;

        private void Awake()
        {
            _inputs = FindObjectOfType<EditorInputs>();
            _editorSpace = GetComponent<EditorSpace>();
        }

        private void Start()
        {
            _editorSpace.TileToPaint
                .Subscribe(UpdateInstance)
                .AddTo(this);

            _inputs.PointerGridPositionStream
                .Where(_ => _ghost != null)
                .Subscribe(Draw)
                .AddTo(this);
            
            _inputs.PointerGridPositionStream
                .Subscribe(UpdateSelectedTile)
                .AddTo(this);
        }

        private void UpdateInstance(TileDescription tileDescription)
        {
            if (_ghost != null)
            {
                Destroy(_ghost.gameObject);
            }

            _ghost = Instantiate(tileDescription.Prefab, transform).AddComponent<TileGhost>();
            _ghost.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, tileDescription.Rotation));
        }

        private void Draw(GridPosition gridPosition)
        {
            TilePainted existentTile = _editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            bool visible = false;

            if (_editorSpace.CanPlace)
            {
                if (existentTile == null)
                {
                    visible = true;
                }
                else if (existentTile.Id != _editorSpace.TileToPaint.Value.Id)
                {
                    if (_inputs.Replace.Value)
                    {
                        visible = true;
                        existentTile.Graphics.color = _overlappedColor;
                    }
                    else
                    {
                        existentTile.Graphics.color = _restColor;
                    }
                }
            }

            if (visible)
            {
                _ghost.transform.position = gridPosition.ToWorld();
                _ghost.Graphics.color = _visibleColor;
            }
            else
            {
                _ghost.Graphics.color = _hiddenColor;
            }
        }

        private void UpdateSelectedTile(GridPosition gridPosition)
        {
            TilePainted tilePainted = _editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            if (tilePainted != _selectedTile)
            {
                if (_selectedTile != null && _selectedTile.Graphics != null)
                {
                    _selectedTile.Graphics.color = _restColor;
                }

                _selectedTile = tilePainted;
            }
        }
    }
}