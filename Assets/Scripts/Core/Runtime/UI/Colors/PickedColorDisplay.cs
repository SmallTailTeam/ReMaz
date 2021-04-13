using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.Core.UI.Colors
{
    public class PickedColorDisplay : MonoBehaviour
    {
        [SerializeField] private ColorPicker _colorPicker;
        [SerializeField, AutoHook] private Image _image;
        
        private void Start()
        {
            _colorPicker.PickedColor
                .Subscribe(ColorChanged)
                .AddTo(this);
        }

        private void ColorChanged(Color color)
        {
            _image.color = color;
        }
    }
}