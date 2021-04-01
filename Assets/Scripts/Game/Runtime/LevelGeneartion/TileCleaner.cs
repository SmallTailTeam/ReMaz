﻿using ReMaz.Core.Grid;
using UniRx;
using UnityEngine;

namespace Game.Runtime.LevelGeneration
{
    public class TileCleaner : MonoBehaviour
    {
        private LevelMovement _worldMovement;

        private void Awake()
        {
            _worldMovement = FindObjectOfType<LevelMovement>();
        }

        private void Start()
        {
            _worldMovement.MovedUnit
                .Subscribe(_ => MovedUnit())
                .AddTo(this);
        }

        private void MovedUnit()
        {
            if (transform.position.x < -ScreenGrid.Size.Value.x * 0.5f - 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}