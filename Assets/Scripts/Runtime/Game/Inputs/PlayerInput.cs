using ReMaz.Game.LevelPlaying;
using UniRx;
using UnityEngine;

namespace ReMaz.Game.Inputs
{
    public class PlayerInput : LevelPlayer
    {
        public ReadOnlyReactiveProperty<int> TrackChange { get; private set; }

        private void Awake()
        {
            TrackChange = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButton(0))
                .Select(_ => GetTrack())
                .ToReadOnlyReactiveProperty();
        }

        private int GetTrack()
        {
            float x = Input.mousePosition.x / Screen.width;

            return Mathf.Clamp(Mathf.CeilToInt(_levelDriver.Level.TrackCount * x), 1, _levelDriver.Level.TrackCount);
        }
    }
}
