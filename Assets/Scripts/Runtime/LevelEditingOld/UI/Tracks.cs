using UnityEngine;

namespace LevelEditingOld.UI
{
    public class Tracks : MonoBehaviour
    {
        [SerializeField] private LevelEditor _levelEditor;
        [SerializeField] private RectTransform _trackPrefab;

        private void Start()
        {
            for (int i = 0; i < _levelEditor.Level.TrackCount; i++)
            {
                RectTransform rt = Instantiate(_trackPrefab, transform);
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, LevelEditorUtils.Height);

                rt.GetComponent<Track>().TrackIndex = i;
            }
        }
    }
}