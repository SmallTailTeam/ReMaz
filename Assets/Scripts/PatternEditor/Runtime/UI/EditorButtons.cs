using UnityEngine;

namespace ReMaz.PatternEditor.UI
{
    public class EditorButtons : MonoBehaviour
    {
        public void SaveProject()
        {
            EditorProject.Save();
        }
    }
}