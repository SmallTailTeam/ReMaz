using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.Core.UI.Colors.Fields
{
    public class RGBFields : MonoBehaviour
    {
        [SerializeField] private ColorPicker _colorPicker;
        [Header("R")]
        [SerializeField] private Slider _rSlider;
        [SerializeField] private TMP_InputField _rInput;
        [Header("G")]
        [SerializeField] private Slider _gSlider;
        [SerializeField] private TMP_InputField _gInput;
        [Header("B")]
        [SerializeField] private Slider _bSlider;
        [SerializeField] private TMP_InputField _bInput;

        private void Start()
        {
            _colorPicker.PickedColor
                .Subscribe(ColorChanged)
                .AddTo(this);
        }

        private void ColorChanged(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255);
            int g = Mathf.RoundToInt(color.g * 255);
            int b = Mathf.RoundToInt(color.b * 255);
            
            _rSlider.value = r;
            _rInput.text = $"{r}";
            
            _gSlider.value = g;
            _gInput.text = $"{g}";
            
            _bSlider.value = b;
            _bInput.text = $"{b}";
        }
    }
}