using ReMaz.Levels;
using UnityEngine;

namespace EditorBehaviours.LevelEditing
{
    public class LevelEditor : MonoBehaviour
    {
        public LevelSelection Level => _levelSelection;
        public LevelEditorMeta Meta { get; private set; }
        
        [Header("Level")]
        [SerializeField] private LevelSelection _levelSelection;
        [Header("Settings")]
        [SerializeField] private int _resolution;
        [SerializeField] private int _scale;

        private void Awake()
        {
            Meta = new LevelEditorMeta();
            Meta.Compute(_levelSelection.LevelSet, _resolution, _scale);
        }
    }
}