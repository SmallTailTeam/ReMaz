using System.Linq;
using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.PatternEditor.Commands;
using ReMaz.PatternEditor.Tiles;
using UniRx;

namespace ReMaz.PatternEditor.Tools
{
    public class EraseTool : TileTool
    {
        private void Start()
        {
            _inputs.PointerGridPositionStream.ToReadOnlyReactiveProperty()
                .Sample(_inputs.EraseStream)
                .Where(_ => _editorSpace.CanPlace && _editorSpace.TileToPaint.Value != null)
                .Subscribe(Use)
                .AddTo(this);
        }

        public override void Use(GridPosition gridPosition)
        {
            EraseCommand command = new EraseCommand(_tileList.TileDatabase, _editorSpace, gridPosition);
            _commandBuffer.Push(command);
        }
    }

    public class EraseCommand : ICommand
    {
        private TileDatabase _tileDatabase;
        private EditorSpace _editorSpace;
        private GridPosition _gridPosition;

        private string _erasedTileId;

        public EraseCommand(TileDatabase tileDatabase, EditorSpace editorSpace, GridPosition gridPosition)
        {
            _tileDatabase = tileDatabase;
            _editorSpace = editorSpace;
            _gridPosition = gridPosition;
        }

        public bool Execute()
        {
            TilePainted tilePainted = _editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(_gridPosition));
            _erasedTileId = tilePainted?.Id;
            
            return _editorSpace.Erase(_gridPosition);
        }

        public void Undo()
        {
            _editorSpace.Paint(_gridPosition, _tileDatabase.FindTile(_erasedTileId));
        }
    }
}