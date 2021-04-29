using System;
using ReMaz.LevelEditingOld;
using ReMaz.Levels;
using UniRx;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelDriver : MonoBehaviour
    {
        public Level Level => _levelSet.Levels[_levelIndex];
        public LevelSet LevelSet => _levelSet;
        public float LevelPosition { get; private set; }

        [SerializeField] private LevelSet _levelSet;
        [SerializeField] private int _levelIndex;

        private ISubject<LevelEvent> _levelEvents = new Subject<LevelEvent>();
        private float _beatLength;

        private void Start()
        {
            _beatLength = _levelSet.Bpm / 60f;
            
            LevelPosition = _beatLength * -5f;
        }

        private void Update()
        {
            LevelPosition += _beatLength * Time.deltaTime;
        }
        
        public IObservable<T> Event<T>() where T : LevelEvent
        {
            return _levelEvents
                .Where(e => e is T)
                .Select(e => e as T);
        }
    }
}