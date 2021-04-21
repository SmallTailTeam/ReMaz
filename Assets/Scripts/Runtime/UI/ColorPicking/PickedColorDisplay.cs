using ReMaz.UI.Windows;
using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.UI.ColorPicking
{
    public class PickedColorDisplay : MonoBehaviour
    {
        [SerializeField] private AttachToWindow _attachToWindow;
        [SerializeField, AutoHook] private Image _image;
        
        private void Start()
        {
            _attachToWindow.Attached
                .Subscribe(Attached)
                .AddTo(this);
        }

        private void Attached(Window window)
        {
            ColorPicker colorPicker = window.GetComponent<ColorPicker>();
            colorPicker.Pick(_image.color);
            
            colorPicker.PickedColor
                .Subscribe(ColorChanged)
                .AddTo(colorPicker);
        }
        
        private void ColorChanged(Color color)
        {
            _image.color = color;
        }
    }
}