using UniRx;
using UnityEngine;

namespace EditorBehaviours.LevelEditing.Controls
{
    public class ControllableLevelScroll : LevelEditorBehaviour
    {
        public ReactiveProperty<int> Scroll { get; private set; }
        
        [SerializeField, Range(0f, 1f)] private float _scrollPercent;

        private ControllableAudioPlayer _audioPlayer;
        
        private int _scrollSpeed;
        private int _scrollCap;

        private void Awake()
        {
            _audioPlayer = FindObjectOfType<ControllableAudioPlayer>();
            
            Scroll = new ReactiveProperty<int>();
        }

        private void Start()
        {
            _scrollCap = _audioPlayer.Source.clip.samples-1;
            _scrollSpeed = Mathf.RoundToInt(_scrollCap * _scrollPercent);
        }

        private void Update()
        {
            ControlledScroll();
            FollowAudio();
        }

        private void FollowAudio()
        {
            int timeSamplesTotal = _audioPlayer.Source.timeSamples * _audioPlayer.Source.clip.channels;
            timeSamplesTotal /= _levelEditor.Meta.WaveformResolution;
            
            Scroll.Value = timeSamplesTotal;
        }
        
        private void ControlledScroll()
        {
            float scrollDelta = Input.mouseScrollDelta.y;

            if (scrollDelta == 0f)
            {
                return;
            }

            float speed = Input.GetKey(KeyCode.LeftShift) ? _scrollSpeed * 10 : _scrollSpeed;
            
            int timeSamplesNew = _audioPlayer.Source.timeSamples + Mathf.RoundToInt(scrollDelta * speed);
            timeSamplesNew = Mathf.Clamp(timeSamplesNew, 0, _scrollCap);

            _audioPlayer.Source.timeSamples = timeSamplesNew;
        }
    }
}