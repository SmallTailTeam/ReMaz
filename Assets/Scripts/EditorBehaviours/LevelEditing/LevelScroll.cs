using UniRx;
using UnityEngine;

namespace ReMaz.EditorBehaviours.LevelEditing
{
    public class LevelScroll : LevelEditorBehaviour
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
            _scrollCap = _audioPlayer.Source.clip.samples;
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
            timeSamplesTotal /= _levelEditor.Meta.Resolution;
            
            Scroll.Value = timeSamplesTotal;
        }
        
        private void ControlledScroll()
        {
            float scrollDelta = Input.mouseScrollDelta.y;

            if (scrollDelta == 0f)
            {
                return;
            }

            int timeSamplesNew = _audioPlayer.Source.timeSamples + Mathf.RoundToInt(scrollDelta * _scrollSpeed);
            timeSamplesNew = Mathf.Clamp(timeSamplesNew, 0, _scrollCap);

            _audioPlayer.Source.timeSamples = timeSamplesNew;
        }
    }
}