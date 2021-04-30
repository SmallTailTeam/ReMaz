using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.EditorBehaviours.LevelEditing.UI
{
    [RequireComponent(typeof(Image))]
    public class WaveformImage : LevelEditorBehaviour
    {
        private LevelScroll _levelScroll;
        
        private Image _image;
        private Material _material;
        private ComputeBuffer _waveBuffer;
        
        private static readonly int Scale = Shader.PropertyToID("_Scale");
        private static readonly int Scroll = Shader.PropertyToID("_Scroll");
        private static readonly int Waveform = Shader.PropertyToID("_Waveform");
        
        private void Awake()
        {
            _levelScroll = FindObjectOfType<LevelScroll>();
            
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _material = _image.material;

            FeedMaterial();

            _levelScroll.Scroll
                .Subscribe(OnScroll)
                .AddTo(this);
        }

        private void FeedMaterial()
        {
            _material.SetInt(Scale, _levelEditor.Meta.Scale);
            
            _waveBuffer = new ComputeBuffer(_levelEditor.Meta.Waveform.Length, sizeof(float));
            _waveBuffer.SetData(_levelEditor.Meta.Waveform);
            
            _material.SetBuffer(Waveform, _waveBuffer);
        }

        private void OnScroll(int scroll)
        {
            _material.SetInt(Scroll, scroll);
        }
    }
}