using UnityEngine;

namespace ReMaz.LevelEditing.UI
{
    public class ScrollWithAudio : MonoBehaviour
    {
        [SerializeField] private bool _offsetEmpty;
        
        private AudioSource _audioSource;
        private RectTransform _rt;
        private float _constantOffset;

        private void Awake()
        {
            _audioSource = FindObjectOfType<AudioSource>();
            _rt = GetComponent<RectTransform>();
            _constantOffset = Screen.height * 0.5f - 50f;
        }

        private void Start()
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x, LevelEditorUtils.Height);
        }

        private void Update()
        {
            float y = (float) _audioSource.timeSamples/_audioSource.clip.samples * LevelEditorUtils.Height;
            y = -y;

            if (_offsetEmpty)
            {
                float offset = (float) LevelEditorUtils.EmptyStart / LevelEditorUtils.MaxScale * LevelEditorUtils.Height;
                y += offset;
            }

            _rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x, y + _constantOffset);
        }
    }
}