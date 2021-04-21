﻿using System.Linq;
using ReMaz.Grid;
using ReMaz.Grid.Tiles;
using ReMaz.MapEditor.Inputs;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.MapEditor.Tiles
{
    public class GhostTileDrawer : MonoBehaviour
    {
        [SerializeField] private Image _colorSource;
        [SerializeField] private Color _visibleColor;
        [SerializeField] private Color _hiddenColor;
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
                .Where(tile => tile != null)
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

            if (_editorSpace.CanPlace && _editorSpace.TileToPaint.Value != null)
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
                        existentTile.Graphics.color = existentTile.Color;
                    }
                }
            }

            if (visible)
            {
                _ghost.transform.position = gridPosition.ToWorld();
                Color color = _colorSource.color;
                color.a = 0.5f;
                _ghost.Graphics.color = color;
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
                    _selectedTile.Graphics.color = _selectedTile.Color;
                }

                _selectedTile = tilePainted;
            }
        }
    }
}