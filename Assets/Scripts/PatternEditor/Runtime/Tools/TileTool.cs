using ReMaz.Core.Content.Projects;
using ReMaz.PatternEditor.Commands;
using ReMaz.PatternEditor.Inputs;
using UnityEngine;

namespace ReMaz.PatternEditor.Tools
{
    public abstract class TileTool : MonoBehaviour
    {
        protected CommandBuffer _commandBuffer;
        protected EditorInputs _inputs;
        protected EditorSpace _editorSpace;

        private void Awake()
        {
            _commandBuffer = FindObjectOfType<CommandBuffer>();
            _inputs = FindObjectOfType<EditorInputs>();
            _editorSpace = GetComponent<EditorSpace>();
        }

        public abstract void Use(GridPosition gridPosition);
    }
}