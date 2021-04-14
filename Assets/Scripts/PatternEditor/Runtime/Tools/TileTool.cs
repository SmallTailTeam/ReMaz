using ReMaz.Core.Content.Projects;
using ReMaz.PatternEditor.Commands;
using ReMaz.PatternEditor.Inputs;
using ReMaz.PatternEditor.UI;
using UnityEngine;

namespace ReMaz.PatternEditor.Tools
{
    public abstract class TileTool : MonoBehaviour
    {
        protected TileList _tileList;
        protected CommandBuffer _commandBuffer;
        protected EditorInputs _inputs;
        protected EditorSpace _editorSpace;

        private void Awake()
        {
            _tileList = FindObjectOfType<TileList>();
            _commandBuffer = FindObjectOfType<CommandBuffer>();
            _inputs = FindObjectOfType<EditorInputs>();
            _editorSpace = GetComponent<EditorSpace>();
        }

        public abstract void Use(GridPosition gridPosition);
    }
}