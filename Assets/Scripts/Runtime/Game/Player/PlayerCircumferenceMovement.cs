using ReMaz.Game.Inputs;
using ReMaz.Game.LevelPlaying;
using TNRD.Autohook;
using UniRx;
using UnityEngine;
using Uween;

namespace ReMaz.Game.Player
{
    public class PlayerCircumferenceMovement : LevelPlayer
    {
        [SerializeField, AutoHook] private PlayerInput _input;
        [SerializeField] private float _interpolationSpeed;

        private void Start()
        {
            _input.TrackChange
                .Subscribe(OnMoved)
                .AddTo(this);
        }

        private void OnMoved(int track)
        {
            float x = Remap(track, 1f, _levelDriver.Level.TrackCount, -1f, 1f);
           
            float duration = 1f / _interpolationSpeed;

            TweenX.Add(gameObject, duration, x);
        }

        private float Remap(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s-a1)*(b2-b1)/(a2-a1);
        }
    }
}