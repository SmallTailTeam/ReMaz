using ReMaz.Core.Content.Projects;
using ReMaz.PatternEditor.Commands;
using UniRx;

namespace ReMaz.PatternEditor.Tools
{
    public class EraseTool : TileTool
    {
        private void Start()
        {
            _inputs.PointerPositionStream.Sample(_inputs.EraseStream)
                .Where(_ => _editorSpace.CanPlace && _editorSpace.TileToPaint != null)
                .Subscribe(Use)
                .AddTo(this);
        }

        public override void Use(GridPosition gridPosition)
        {
            EraseCommand command = new EraseCommand(_editorSpace, gridPosition);
            _commandBuffer.Push(command);
        }
    }

    public class EraseCommand : ICommand
    {
        private EditorSpace _editorSpace;
        private GridPosition _gridPosition;

        public EraseCommand(EditorSpace editorSpace, GridPosition gridPosition)
        {
            _editorSpace = editorSpace;
            _gridPosition = gridPosition;
        }
        
        public bool Execute()
        {
            return EditorUtils.Erase(_gridPosition, _editorSpace);
        }

        public void Undo()
        {
            EditorUtils.Paint(_gridPosition, _editorSpace, _editorSpace.TileToPaint.Value);
        }
    }
}