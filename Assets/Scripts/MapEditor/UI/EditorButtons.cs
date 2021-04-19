using ReMaz.Grid.Maps;
using UnityEngine;

namespace ReMaz.MapEditor.UI
{
    public class EditorButtons : MonoBehaviour
    {
        private IEditor<Map> _editor;

        private void Awake()
        {
            _editor = FindObjectOfType<MapEditor>();
        }

        public void SaveProject()
        {
            _editor.Save();
        }
    }
}