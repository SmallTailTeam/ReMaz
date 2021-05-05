using EditorBehaviours.LevelEditing.Controls;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EditorBehaviours.LevelEditing.UI
{
    [RequireComponent(typeof(Image))]
    public class WaveformImage : LevelEditorBehaviour
    {
        private ControllableLevelScroll _scroll;
        
        private Image _image;
        private Material _material;
        private ComputeBuffer _waveBuffer;
        private int _offset;
        
        private static readonly int ViewSize = Shader.PropertyToID("_ViewSize");
        private static readonly int Scroll = Shader.PropertyToID("_Scroll");
        private static readonly int Waveform = Shader.PropertyToID("_Waveform");
        
        private void Awake()
        {
            _scroll = FindObjectOfType<ControllableLevelScroll>();
            
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _offset = _levelEditor.Meta.ViewSize / 2;
            
            _material = _image.material;

            FeedMaterial();

            _scroll.Scroll
                .Subscribe(OnScroll)
                .AddTo(this);
        }

        private void FeedMaterial()
        {
            _material.SetInt(ViewSize, _levelEditor.Meta.ViewSize);
            
            _waveBuffer = new ComputeBuffer(_levelEditor.Meta.Waveform.Length, sizeof(float));
            _waveBuffer.SetData(_levelEditor.Meta.Waveform);
            
            _material.SetBuffer(Waveform, _waveBuffer);
        }

        private void OnScroll(int scroll)
        {
            _material.SetInt(Scroll, scroll - _offset);
        }
    }
}