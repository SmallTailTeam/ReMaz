using UnityEngine;
using UnityEngine.UI;

namespace EditorBehaviours.LevelEditing.UI.Beats
{
    public class BeatDisplay : MonoBehaviour
    {
        private RectTransform _rt;
        private Text _text;

        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
            _text = GetComponentInChildren<Text>();
        }

        public void Display(int beat)
        {
            _text.text = $"{beat}";
        }

        public void Position(float position)
        {
            _rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x, position);
        }

        public void Size(float beatLength)
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x, beatLength);
        }
    }
}