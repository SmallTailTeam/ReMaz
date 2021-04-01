using UnityEngine;

namespace ReMaz.Core.UI.Windows
{
    public class WindowManager : MonoBehaviour
    {
        public void Open(Window window)
        {
            Window instance = Instantiate(window, transform);
            instance.Open();
        }

        public void Clear()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Window window))
                {
                    window.Close();
                }
            }
        }
    }
}