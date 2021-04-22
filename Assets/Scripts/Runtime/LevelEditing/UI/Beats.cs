using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.LevelEditing.UI
{
    public class Beats : MonoBehaviour
    {
        [SerializeField] private LevelEditor _levelEditor;
        [SerializeField] private RectTransform _beatPrefab;

        private LevelScroll _levelScroll;
        private AudioSource _audioSource;
        private RectTransform _self;
        private float _height;
        private float _beatLength;

        private void Awake()
        {
            _levelScroll = FindObjectOfType<LevelScroll>();
            _audioSource = FindObjectOfType<AudioSource>();
            _self = GetComponent<RectTransform>();
        }

        private void Start()
        {
            AudioClip clip = _levelEditor.Levelset.Clip;
            
            float minutes = clip.samples / clip.frequency / 60f;
            int beats = Mathf.RoundToInt(minutes * _levelEditor.Levelset.Bpm);
            
            _beatLength = _levelEditor.Levelset.Bpm / 60f * _levelScroll.MaxScale / Screen.height;

            for (int i = 0; i < beats; i++)
            {
                RectTransform beatInstance = Instantiate(_beatPrefab, transform);
                Text text = beatInstance.GetComponentInChildren<Text>();
                text.text = $"{i + 1}";
                RectTransform textRT = text.GetComponent<RectTransform>();
                textRT.sizeDelta = new Vector2(textRT.sizeDelta.x, _beatLength * 0.45f);
                textRT.anchoredPosition = new Vector2(textRT.anchoredPosition.x, -textRT.sizeDelta.y * 0.5f);

                beatInstance.sizeDelta = new Vector2(beatInstance.sizeDelta.x, _beatLength);
                beatInstance.anchoredPosition = new Vector2(0f, i * _beatLength);
            }

            _height = beats * _beatLength;
        }

        private void Update()
        {
            int resolution = _audioSource.clip.frequency / LevelEditorSettings.Resolution;
            
            float y = (float) _audioSource.timeSamples/resolution / _levelScroll.Scale.Value * _height / 5f;
            //float offset = (float) LevelEditorSettings.Empty / _levelScroll.MaxScale * _height;

            _self.anchoredPosition = new Vector2(0f, -y);
        }
    }
}