﻿using ReMaz.Grid;
using ReMaz.Grid.Tiles;
using ReMaz.MapEditor.Commands;
using UniRx;

namespace ReMaz.MapEditor.Tools
{
    public class PaintTool : TileTool
    {
        private void Start()
        {
            _inputs.PointerGridPositionStream.ToReadOnlyReactiveProperty()
                .Sample(_inputs.PaintStream)
                .Where(_ => _editorSpace.CanPlace && _editorSpace.TileToPaint.Value != null)
                .Subscribe(Use)
                .AddTo(this);
        }

        public override void Use(GridPosition gridPosition)
        {
            PaintCommand command = new PaintCommand(_tileList.TileDatabase, _editorSpace, gridPosition);
            _commandBuffer.Push(command);
        }
    }
    
    public class PaintCommand : ICommand
    {
        private TileDatabase _tileDatabase;
        private EditorSpace _editorSpace;
        private GridPosition _gridPosition;

        private string _paintedTileId;

        public PaintCommand(TileDatabase tileDatabase, EditorSpace editorSpace, GridPosition gridPosition)
        {
            _tileDatabase = tileDatabase;
            _editorSpace = editorSpace;
            _gridPosition = gridPosition;
        }
        
        public bool Execute()
        {
            if (string.IsNullOrWhiteSpace(_paintedTileId))
            {
                _paintedTileId = _editorSpace.TileToPaint.Value.Id;
            }

            return _editorSpace.Paint(_gridPosition, _tileDatabase.FindTile(_paintedTileId));
        }

        public void Undo()
        {
            _editorSpace.Erase(_gridPosition);
        }
    }
}