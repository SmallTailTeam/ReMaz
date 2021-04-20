using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ReMaz.Levels
{
    [CreateAssetMenu(menuName = "ReMaz!/Levels/Level", fileName = "Level")]
    public class Level : ScriptableObject
    {
        public IObservable<BpmChangeEvent> BpmChanges => _bpmChanges;
        public IObservable<SpawnEvent> Spawns => _spawns;
        
        public string Name;
        public List<BpmChangeEvent> BpmChangeEvents;
        public List<SpawnEvent> SpawnEvents;

        private ISubject<BpmChangeEvent> _bpmChanges = new Subject<BpmChangeEvent>();
        private ISubject<SpawnEvent> _spawns = new Subject<SpawnEvent>();
        
        public void Drive(float t1, float t2)
        {
            RunEvents(BpmChangeEvents, _bpmChanges, t1, t2);
            RunEvents(SpawnEvents, _spawns, t1, t2);
        }

        private void RunEvents<T>(List<T> events, ISubject<T> subject, float t1, float t2) where T : LevelEvent
        {
            foreach (T levelEvent in events)
            {
                if (levelEvent.Time == 0f && t1 == 0f)
                {
                    subject.OnNext(levelEvent);
                    continue;
                }
                
                if (levelEvent.Time > t1 && levelEvent.Time < t2)
                {
                    subject.OnNext(levelEvent);
                }
            }
        }
    }
}