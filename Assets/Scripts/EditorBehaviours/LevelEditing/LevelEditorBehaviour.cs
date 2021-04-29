using UnityEngine;

namespace ReMaz.EditorBehaviours.LevelEditing
{
    public class LevelEditorBehaviour : MonoBehaviour
    {
        protected LevelEditor _levelEditor
        {
            get
            {
                if (__levelEditor == null)
                {
                    __levelEditor = FindObjectOfType<LevelEditor>();
                }

                return __levelEditor;
            }
        }

        private LevelEditor __levelEditor;
    }
}