﻿using System;
using ReMaz.Core.Grid;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Inputs
{
    public abstract class EditorInputs : MonoBehaviour
    {
        public abstract IObservable<Unit> PaintStream { get; protected set; }
        public abstract IObservable<Unit> EraseStream { get; protected set; }
        public abstract ReadOnlyReactiveProperty<bool> Replace { get; protected set; }
        public abstract IObservable<GridPosition> PointerPositionStream { get; protected set; }
        public abstract IObservable<float> ScrollStream { get; protected set;  }
    }
}