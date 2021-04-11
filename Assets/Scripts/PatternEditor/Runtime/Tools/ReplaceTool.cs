using System.Linq;
using ReMaz.Core.Content.Projects;
using ReMaz.PatternEditor.Commands;
using ReMaz.PatternEditor.Tiles;
using UniRx;

namespace ReMaz.PatternEditor.Tools
{
    public class ReplaceTool : TileTool
    {
        private void Start()
        {
            _inputs.PointerGridPositionStream.ToReadOnlyReactiveProperty()
                .Sample(_inputs.PaintStream)
                .Where(_ => _inputs.Replace.Value
                            && _editorSpace.CanPlace
                            && _editorSpace.TileToPaint != null)
                .Subscribe(Use)
                .AddTo(this);
        }

        public override void Use(GridPosition gridPosition)
        {
            ReplaceCommand command = new ReplaceCommand(_editorSpace, gridPosition);
            _commandBuffer.Push(command);
        }
    }

    public class ReplaceCommand : ICommand
    {
        private EditorSpace _editorSpace;
        private GridPosition _gridPosition;
        
        private string _existentTileId;

        public ReplaceCommand(EditorSpace editorSpace, GridPosition gridPosition)
        {
            _editorSpace = editorSpace;
            _gridPosition = gridPosition;
        }
        
        public bool Execute()
        {
            TilePainted existentTile = _editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(_gridPosition));

            _existentTileId = existentTile?.Id ?? _editorSpace.TileToPaint.Value.Id;
            
            if (_existentTileId != _editorSpace.TileToPaint.Value.Id)
            {
                _editorSpace.Erase(_gridPosition);
                _editorSpace.Paint(_gridPosition, _editorSpace.TileToPaint.Value);

                return true;
            }

            return false;
        }

        public void Undo()
        {
            _editorSpace.Erase(_gridPosition);
            _editorSpace.Paint(_gridPosition, _editorSpace.TileDatabase.FindTile(_existentTileId));
        }
    }
}