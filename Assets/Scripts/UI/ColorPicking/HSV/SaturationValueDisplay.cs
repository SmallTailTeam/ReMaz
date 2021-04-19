using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.UI.ColorPicking.HSV
{
    public class SaturationValueDisplay : MonoBehaviour
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] private HSVColorPicker _colorPicker;
        
        private Material _material;
        
        private static readonly int Hue = Shader.PropertyToID("_Hue");

        private void Awake()
        {
            Image image = GetComponent<Image>();
            
            _material = Instantiate(image.material);
            image.material = _material;
        }

        private void Start()
        {
            _colorPicker.H
                .Subscribe(hue => _material.SetInt(Hue, Mathf.RoundToInt(hue * 359f)))
                .AddTo(this);
        }
    }
}