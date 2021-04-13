using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.Colors
{
    public class HueKnob : MonoBehaviour
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] private HuePicker _huePicker;
        [SerializeField, AutoHook] private RectTransform _rectTransform;

        private void Start()
        {
            _huePicker.AngleChanged
                .Subscribe(AngleChanged)
                .AddTo(this);
        }

        private void AngleChanged(float angle)
        {
            float rad = 2f * Mathf.PI * angle;
            Vector2 position = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            _rectTransform.localPosition = position * 67f;
        }
    }
}