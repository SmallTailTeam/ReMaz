using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.UI.ColorPicking.Modes.HSV
{
    public class HueMaterialProvider : MonoBehaviour
    {
        [SerializeField] private ColorPicker _colorPicker;
        [SerializeField] private List<Image> _images;

        private List<Material> _materials;
        
        private static readonly int Hue = Shader.PropertyToID("_Hue");

        private void Awake()
        {
            _materials = new List<Material>();
            
            foreach (Image image in _images)
            {
                Material material = Instantiate(image.material);
                image.material = material;
                _materials.Add(material);
            }
        }

        private void Start()
        {
            _colorPicker.PickedColor
                .Subscribe(Provide)
                .AddTo(this);
        }

        private void Provide(Color color)
        {
            _colorPicker.AsHSV(out float h, out float _, out float _);

            int value = Mathf.RoundToInt(h * 360f);

            foreach (Material material in _materials)
            {
                material.SetInt(Hue, value);
            }
        }
    }
}