using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReMaz.Core.Scenes
{
    public class SceneChanger : MonoBehaviour
    {
        public static void Change(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}