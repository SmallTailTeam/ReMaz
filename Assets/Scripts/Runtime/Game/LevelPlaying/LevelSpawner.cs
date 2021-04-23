using System.Collections.Generic;
using ReMaz.Game.Pooling;
using ReMaz.Levels;
using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelSpawner : LevelPlayer
    {
        [SerializeField, AutoHook] private LevelMovement _levelMovement;
        [SerializeField] private float _timeBeforehand;

        private DynamicPool _pool = new DynamicPool();
        private HashSet<SpawnEvent> _pastEvents = new HashSet<SpawnEvent>();
        private float _spawnDistance;

        private void Start()
        {
            _spawnDistance = _levelMovement.Speed * _timeBeforehand;
        }

        private void Update()
        {
            float t1 = _levelDriver.LevelTime + _timeBeforehand;
            
            foreach (LevelEvent levelEvent in _levelDriver.Level.Events)
            {
                if (levelEvent is SpawnEvent spawnEvent && !_pastEvents.Contains(spawnEvent))
                {
                    if (levelEvent.Time <= t1)
                    {
                        Spawn(spawnEvent);
                        _pastEvents.Add(spawnEvent);
                    }
                }
            }
        }

        private void Spawn(SpawnEvent e)
        {
            GameObject instance = _pool.Request(e.Prefab, transform);
            Transform itransform = instance.transform;

            Vector3 worldMovement = new Vector3(e.Track-1, 0f, (e.Time - _levelDriver.LevelTime) * _levelMovement.Speed);
            itransform.position = worldMovement;
            Vector3 parentMovement = new Vector3(0f, -1.2f, 0f);
            itransform.localPosition += parentMovement;
        }
    }
}