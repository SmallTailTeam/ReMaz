using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.Core.UI.Colors
{
    public class SaturationValueDisplay : MonoBehaviour
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] private ColorPicker _colorPicker;
        
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