using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReMaz.UI.Windows
{
    public class AttachToScene : MonoBehaviour
    {
        private Window _window;

        private void Awake()
        {
            _window = GetComponent<Window>();
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += SceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= SceneChanged;
        }

        private void SceneChanged(Scene _, Scene __)
        {
            _window.Close();
        }
    }
}