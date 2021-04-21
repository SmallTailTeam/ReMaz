using UnityEngine;

namespace ReMaz.UI.Windows
{
    public class WindowButtons : MonoBehaviour
    {
        private WindowManager _windowManager;

        private void Awake()
        {
            _windowManager = FindObjectOfType<WindowManager>();
        }

        public void Open(Window window)
        {
            _windowManager.Open(window);
        }

        public void Close(Window window)
        {
            window.Close();
        }

        public void Clear()
        {
            _windowManager.Clear();
        }
    }
}