using System;
using ReMaz.Levels;
using UniRx;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelDriver : MonoBehaviour
    {
        public Level Level => _levelSet.Levels[_levelIndex];
        public LevelSet LevelSet => _levelSet;
        public float LevelTime { get; private set; } = -5f;

        [SerializeField] private LevelSet _levelSet;
        [SerializeField] private int _levelIndex;

        private ISubject<LevelEvent> _levelEvents = new Subject<LevelEvent>();

        private void Update()
        {
            float nextTime = LevelTime + Time.deltaTime;
            //Drive(LevelTime, nextTime);
            LevelTime = nextTime;
        }
        
        private void Drive(float t1, float t2)
        {
            foreach (LevelEvent levelEvent in Level.Events)
            {
                if (levelEvent.Time == 0f && t1 == 0f)
                {
                    _levelEvents.OnNext(levelEvent);
                    continue;
                }
                
                if (levelEvent.Time > t1 && levelEvent.Time < t2)
                {
                    _levelEvents.OnNext(levelEvent);
                }
            }
        }

        public IObservable<T> Event<T>() where T : LevelEvent
        {
            return _levelEvents
                .Where(e => e is T)
                .Select(e => e as T);
        }
    }
}