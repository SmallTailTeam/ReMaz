using ReMaz.Core.Content.Projects;
using ReMaz.PatternEditor.Inputs;
using UnityEngine;

namespace ReMaz.PatternEditor.Tools
{
    public abstract class TileTool : MonoBehaviour
    {
        protected EditorInputs _inputs;
        protected EditorSpace _editorSpace;

        private void Awake()
        {
            _inputs = FindObjectOfType<EditorInputs>();
            _editorSpace = GetComponent<EditorSpace>();
        }

        public abstract void Use(GridPosition gridPosition);
    }
}