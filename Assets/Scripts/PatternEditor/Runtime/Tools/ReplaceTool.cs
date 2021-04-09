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
            _inputs.PointerPositionStream.ToReadOnlyReactiveProperty()
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
                EditorUtils.Erase(_gridPosition, _editorSpace);
                EditorUtils.Paint(_gridPosition, _editorSpace, _editorSpace.TileToPaint.Value);

                return true;
            }

            return false;
        }

        public void Undo()
        {
            EditorUtils.Erase(_gridPosition, _editorSpace);
            EditorUtils.Paint(_gridPosition, _editorSpace, _editorSpace.TileDatabase.FindTile(_existentTileId));
        }
    }
}