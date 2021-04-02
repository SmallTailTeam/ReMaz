using System;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Game
{
    public class LevelMovement : MonoBehaviour
    {
        public IObservable<float> Moved => _moved;
        public IObservable<float> MovedUnit => _movedUnit;
        
        [SerializeField, AutoHook] private AudioSpeed _audioSpeed;
        [SerializeField] private float _baseSpeed;
        
        private Subject<float> _moved;
        private Subject<float> _movedUnit;
        private float _position;

        private void Awake()
        {
            _moved = new Subject<float>();
            _movedUnit = new Subject<float>();
        }
        
        private void Update()
        {
            float speed = _baseSpeed * _audioSpeed.GetAudioMultiplier();
            float movement = speed * Time.deltaTime;
            
            _position += movement;
            
            if (_position >= 1f)
            {
                float compensation = (_position - 1f) * speed;
                _movedUnit.OnNext(compensation);
                _position = 0f;
            }
            
            _moved.OnNext(movement);
        }
    }
}