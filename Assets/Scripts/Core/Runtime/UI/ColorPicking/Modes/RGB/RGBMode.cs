using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.ColorPicking.Modes.RGB
{
    public class RGBMode : MonoBehaviour
    {
        [SerializeField] private ColorPicker _colorPicker;
        [SerializeField] private RGBField _rField;
        [SerializeField] private RGBField _gField;
        [SerializeField] private RGBField _bField;

        private void Start()
        {
            _colorPicker.PickedColor
                .Subscribe(ColorChanged)
                .AddTo(this);

            Observable.Merge(_rField.ValueChanged, _gField.ValueChanged, _bField.ValueChanged)
                .Subscribe(_ => _colorPicker.Pick(new Color(_rField.Value / 255f, _gField.Value / 255f, _bField.Value / 255f)))
                .AddTo(this);
        }

        private void ColorChanged(Color color)
        {
            _rField.Changed(color.r);
            _gField.Changed(color.g);
            _bField.Changed(color.b);
        }
    }
}