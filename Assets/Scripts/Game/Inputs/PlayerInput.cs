using UniRx;
using UnityEngine;

namespace ReMaz.Game.Inputs
{
    public class PlayerInput : MonoBehaviour
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

            int track;

            if (x < 0.4f)
            {
                track = -1;
            }
            else if (x > 0.6f)
            {
                track = 1;
            }
            else
            {
                track = 0;
            }

            return track;
        }
    }
}
