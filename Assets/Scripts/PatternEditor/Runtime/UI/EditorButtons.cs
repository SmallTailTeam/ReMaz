using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Patterns;
using UnityEngine;

namespace ReMaz.PatternEditor.UI
{
    public class EditorButtons : MonoBehaviour
    {
        private IProjectEditor<IProject<Pattern>> _projectEditor;

        private void Awake()
        {
            _projectEditor = FindObjectOfType<PatternProjectEditor>();
        }

        public void SaveProject()
        {
            _projectEditor.Save();
        }
    }
}