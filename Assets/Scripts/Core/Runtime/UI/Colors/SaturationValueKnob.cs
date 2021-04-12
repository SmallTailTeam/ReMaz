using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.Colors
{
    public class SaturationValueKnob : MonoBehaviour
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] private SaturationValuePicker _picker;

        [SerializeField, AutoHook] private RectTransform _rectTransform;

        private void Start()
        {
            _picker.PositionChanged
                .Subscribe(PositionChanged)
                .AddTo(this);
        }

        private void PositionChanged(Vector2 position)
        {
            _rectTransform.position = position + new Vector2(-_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y);
        }
    }
}