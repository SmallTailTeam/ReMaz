using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ReMaz.Scenes
{
    public class SceneButtons : MonoBehaviour
    {
        private SceneChanger _sceneChanger;

        private void Awake()
        {
            _sceneChanger = FindObjectOfType<SceneChanger>();
        }

        public void Change(string sceneName)
        {
            _sceneChanger.LoadScene(sceneName).Forget();
        }
    }
}