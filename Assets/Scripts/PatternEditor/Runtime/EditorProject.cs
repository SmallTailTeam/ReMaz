using Remaz.Game.Grid;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorProject : MonoBehaviour
    {
        public static Pattern CurrentProject;

        public static void Open(Pattern pattern)
        {
            CurrentProject = pattern;
        }

        public static void Create()
        {
            CurrentProject = new Pattern();
        }

        private void Awake()
        {
            if (CurrentProject == null)
            {
                Create();
            }
        }
    }
}