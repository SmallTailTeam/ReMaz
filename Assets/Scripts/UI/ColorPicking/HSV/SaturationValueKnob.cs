using UniRx;
using UnityEngine;

namespace ReMaz.UI.ColorPicking.HSV
{
    public class SaturationValueKnob : ValueKnob
    {
        [SerializeField] private RectTransform _container;

        private void Start()
        {
            _colorPicker.S.Merge(_colorPicker.V)
                .Subscribe(_ => SVChanged())
                .AddTo(this);
        }

        private void SVChanged()
        {
            float x = _colorPicker.S.Value * _container.sizeDelta.x - _container.sizeDelta.x * 0.5f - _rectTransform.sizeDelta.x * 0.5f;
            float y = _colorPicker.V.Value * _container.sizeDelta.y - _container.sizeDelta.y * 0.5f + _rectTransform.sizeDelta.y * 0.5f;
            
            _rectTransform.localPosition = new Vector3(x, y, _rectTransform.localPosition.z);
        }
    }
}