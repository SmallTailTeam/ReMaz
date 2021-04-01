﻿using System;
using UniRx;
using UnityEngine;

namespace Game.Runtime
{
    public class LevelMovement : MonoBehaviour
    {
        public IObservable<float> Moved => _moved;
        public IObservable<Unit> MovedUnit => _movedUnit;
        
        [SerializeField] private float _baseSpeed;

        private Subject<float> _moved;
        private Subject<Unit> _movedUnit;
        private float _position;

        private void Awake()
        {
            _moved = new Subject<float>();
            _movedUnit = new Subject<Unit>();
        }
        
        private void Update()
        {
            float movement = _baseSpeed * Time.deltaTime;
            
            _position += movement;
            
            if (_position >= 1f)
            {
                _movedUnit.OnNext(Unit.Default);
                _position = 0f;
            }
            
            _moved.OnNext(movement);
        }
    }
}