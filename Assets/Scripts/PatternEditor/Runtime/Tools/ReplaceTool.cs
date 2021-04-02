using System.Linq;
using ReMaz.Core.ContentContainers.Projects;
using ReMaz.PatternEditor.Tiles;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Tools
{
    public class ReplaceTool : TileTool
    {
        [SerializeField] private TileTool _paintTool;
        [SerializeField] private TileTool _eraseTool;
        
        private void Start()
        {
            _inputs.PointerPositionStream.Sample(_inputs.PaintStream)
                .Where(_ => _inputs.Replace.Value
                            && _editorSpace.CanPlace
                            && _editorSpace.TileToPaint != null)
                .Subscribe(Use)
                .AddTo(this);
        }

        public override void Use(GridPosition gridPosition)
        {
            TilePainted existentTile = _editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            if ((existentTile?.Id ?? _editorSpace.TileToPaint.Value.Id) != _editorSpace.TileToPaint.Value.Id)
            {
                _eraseTool.Use(gridPosition);
                _paintTool.Use(gridPosition);
            }
        }
    }
}