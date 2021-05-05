using System.Collections.Generic;
using Game.Pooling;
using ReMaz.Levels;
using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelSpawner : LevelPlayer
    {
        [SerializeField, AutoHook] private LevelMovement _levelMovement;
        [SerializeField] private float _timeBeforehand;

        private DynamicObjectPool _objectPool = new DynamicObjectPool();
        private HashSet<SpawnEvent> _pastEvents = new HashSet<SpawnEvent>();
        private float _spawnDistance;

        private void Start()
        {
            _spawnDistance = _levelMovement.Speed * _timeBeforehand;
        }

        private void Update()
        {
            float t1 = _levelDriver.LevelPosition + _timeBeforehand;
            
            foreach (LevelEvent levelEvent in _levelDriver.Level.Events)
            {
                if (levelEvent is SpawnEvent spawnEvent && !_pastEvents.Contains(spawnEvent))
                {
                    if (levelEvent.Position <= t1)
                    {
                        Spawn(spawnEvent);
                        _pastEvents.Add(spawnEvent);
                    }
                }
            }
        }

        private void Spawn(SpawnEvent e)
        {
            GameObject instance = _objectPool.Request(e.Prefab, transform);
            Transform itransform = instance.transform;

            Vector3 worldMovement = new Vector3(e.Track-1, 0f, (e.Position - _levelDriver.LevelPosition) * _levelDriver.Level.DistanePerBeat);
            itransform.position = worldMovement;
            Vector3 parentMovement = new Vector3(0f, -1.2f, 0f);
            itransform.localPosition += parentMovement;
        }
    }
}