using UnityEngine;

namespace ReMaz.Core.Scenes
{
    public class SceneButtons : MonoBehaviour
    {
        public void Change(string name)
        {
            SceneChanger.Change(name);
        }
    }
}