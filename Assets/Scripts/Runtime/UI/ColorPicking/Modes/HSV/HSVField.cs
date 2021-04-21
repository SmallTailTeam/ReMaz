using TMPro;
using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.UI.ColorPicking.Modes.HSV
{
    public class HSVField : Bindable<float>
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private Slider _slider;
        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private TMP_InputField _input;
        [SerializeField] private float _cap;

        private void Awake()
        {
            _slider.onValueChanged.AsObservable()
                .Subscribe(SliderChange)
                .AddTo(this);

            _input.onValueChanged.AsObservable()
                .Subscribe(InputChange)
                .AddTo(this);
        }

        private void SliderChange(float value)
        {
            InternalChange(value / _cap);
        }

        private void InputChange(string text)
        {
            if (!int.TryParse(text, out int value))
            {
                return;
            }

            if (value > _cap)
            {
                _input.text = $"{_cap}";
                return;
            }

            if (value < 0)
            {
                _input.text = "0";
                return;
            }
            
            InternalChange(value / _cap);
        }

        protected override void Change(float value)
        {
            int round = Mathf.RoundToInt(value * _cap);
            
            _slider.value = round;
            _input.text = $"{round}";
        }
    }
}