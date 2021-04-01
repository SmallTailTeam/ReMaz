﻿using System.Linq;
using ReMaz.Core.Grid;
using ReMaz.Core.Grid.Tiles;
using ReMaz.PatternEditor.Tiles;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Tools
{
    public class PaintTool : TileTool
    {
        private void Start()
        {
            _inputs.PointerPositionStream.Sample(_inputs.PaintStream)
                .Where(_ => _editorSpace.CanPlace && _editorSpace.TileToPaint != null)
                .Subscribe(Use)
                .AddTo(this);
        }

        public override void Use(GridPosition gridPosition)
        {
            TilePainted existentTile = _editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            if (existentTile == null)
            {
                TileDescription tileToPaint = _editorSpace.TileToPaint.Value;
                
                GameObject instance = Instantiate(tileToPaint.Prefab, transform);
                instance.transform.position = gridPosition.ToWorld();
                instance.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, tileToPaint.Rotation));

                TilePainted tilePainted = new TilePainted(tileToPaint.Id, instance, gridPosition)
                {
                    Graphics = instance.GetComponentInChildren<SpriteRenderer>()
                };
                _editorSpace.Painted.Add(tilePainted);

                TileSpatial tileSpatial = new TileSpatial(tileToPaint.Id, gridPosition);
                EditorProject.CurrentProject.Pattern.Tiles.Add(tileSpatial);
            }
        }
    }
}