using System;
using TMPro;
using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.UI.ColorPicking.Modes.RGB
{
    public class RGBField : MonoBehaviour
    {
        public IObservable<float> ValueChanged => _valueChanged;
        public float Value => _value;
        
        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private Slider _slider;
        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private TMP_InputField _input;

        private ISubject<float> _valueChanged = new Subject<float>();
        private int _value;
        
        private void Start()
        {
            _input.onValueChanged.AsObservable()
                .Subscribe(text => InputChanged(int.Parse(string.IsNullOrWhiteSpace(text) ? "0" : text)))
                .AddTo(this);

            _slider.onValueChanged.AsObservable()
                .Subscribe(value => InputChanged((int)value))
                .AddTo(this);
        }

        private void InputChanged(int value)
        {
            if (value > 255)
            {
                _input.text = "255";
                return;
            }
            if (value < 0)
            {
                _input.text = "0";
                return;
            }

            if (_value == value)
            {
                return;
            }

            _value = value;
            _valueChanged?.OnNext(_value);
        }
        
        public void Changed(float value)
        {
            _value = Mathf.RoundToInt(value * 255f);
            
            _slider.value = _value;
            _input.text = $"{_value}";
        }
    }
}