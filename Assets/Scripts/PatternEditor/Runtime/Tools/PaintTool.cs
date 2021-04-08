using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.PatternEditor.Commands;
using UniRx;

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
            
            foreach (TileSpatial tile in EditorProject.CurrentProject.Content.Tiles)
            {
                EditorUtils.CreateInstance(tile.Position, _editorSpace.TileDatabase.FindTile(tile.Id), _editorSpace);
            }
        }

        public override void Use(GridPosition gridPosition)
        {
            PaintCommand command = new PaintCommand(_editorSpace, gridPosition);
            _commandBuffer.Push(command);
        }
    }
    
    public class PaintCommand : ICommand
    {
        private EditorSpace _editorSpace;
        private GridPosition _gridPosition;

        public PaintCommand(EditorSpace editorSpace, GridPosition gridPosition)
        {
            _editorSpace = editorSpace;
            _gridPosition = gridPosition;
        }
        
        public bool Execute()
        {
            return EditorUtils.Paint(_gridPosition, _editorSpace, _editorSpace.TileToPaint.Value);
        }

        public void Undo()
        {
            EditorUtils.Erase(_gridPosition, _editorSpace);
        }
    }
}