using System.Linq;
using Remaz.Core.Grid;
using Remaz.Core.Grid.Tiles;
using ReMaz.PatternEditor.Tiles;
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
            TilePainted tilePainted = _editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
            
            if (tilePainted != null)
            {
                Destroy(tilePainted.Instance);
                _editorSpace.Painted.Remove(tilePainted);

                TileSpatial tileSpatial = EditorProject.CurrentProject.Pattern.Tiles.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
                EditorProject.CurrentProject.Pattern.Tiles.Remove(tileSpatial);
            }
        }
    }
}