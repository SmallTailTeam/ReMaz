using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.LevelEditing
{
    public class LevelScroll : MonoBehaviour
    {
        public ReactiveProperty<int> Scale { get; } = new ReactiveProperty<int>();
        public int MaxScale => _maxScale;
        
        [SerializeField, AutoHook] private LevelEditor _levelEditor;
        [SerializeField] private int _scaleSpeed = 100;

        private int _maxScale;
        
        private void Awake()
        {
            AudioClip clip = _levelEditor.Levelset.Clip;
            int resolution = clip.frequency / LevelEditorSettings.Resolution;
            int wtf = clip.samples * clip.channels;
            _maxScale = wtf/resolution;
            Scale.Value = Mathf.RoundToInt(_maxScale * 0.1f);
        }

        private void Update()
        {
            //DoScale();
        }

        private void DoScale()
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                return;
            }
            
            int y = -Mathf.RoundToInt(Input.mouseScrollDelta.y);

            if (y == 0)
            {
                return;
            }
            
            y *= _scaleSpeed;

            int newValue = Scale.Value + y;

            newValue = Mathf.Clamp(newValue, 500, _maxScale);
            
            Scale.Value = newValue;
        }
    }
}