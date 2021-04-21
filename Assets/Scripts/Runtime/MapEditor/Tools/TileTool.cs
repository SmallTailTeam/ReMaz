using ReMaz.Grid;
using ReMaz.MapEditor.Commands;
using ReMaz.MapEditor.Inputs;
using ReMaz.MapEditor.UI;
using UnityEngine;

namespace ReMaz.MapEditor.Tools
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