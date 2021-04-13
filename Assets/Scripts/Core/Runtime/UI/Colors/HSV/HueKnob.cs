using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.Colors.HSV
{
    public class HueKnob : ValueKnob
    {
        [SerializeField] private float _radius;

        private void Start()
        {
            _colorPicker.H
                .Subscribe(HChanged)
                .AddTo(this);
        }

        private void HChanged(float h)
        {
            float rad = 2f * Mathf.PI * h;
            Vector2 position = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            _rectTransform.localPosition = position * _radius;
        }
    }
}