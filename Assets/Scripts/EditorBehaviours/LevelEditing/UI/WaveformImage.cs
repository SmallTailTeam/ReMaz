using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.EditorBehaviours.LevelEditing.UI
{
    [RequireComponent(typeof(Image))]
    public class WaveformImage : LevelEditorBehaviour
    {
        private Image _image;
        private Material _material;
        private float[] _waveform;
        
        private static readonly int Length = Shader.PropertyToID("_Length");
        private static readonly int Waveform = Shader.PropertyToID("_Waveform");
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _material = _image.material;
            _waveform = _levelEditor.Meta.Waveform;
            
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _material.SetInt(Length, _waveform.Length);

            ComputeBuffer buffer = new ComputeBuffer(_waveform.Length, sizeof(float));
            buffer.SetData(_waveform);
            
            _material.SetBuffer(Waveform, buffer);
        }
    }
}