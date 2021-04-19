using System;
using ReMaz.Grid;
using UniRx;
using UnityEngine;

namespace ReMaz.MapEditor.Inputs
{
    public abstract class EditorInputs : MonoBehaviour
    {
        public abstract IObservable<Unit> PaintStream { get; protected set; }
        public abstract IObservable<Unit> EraseStream { get; protected set; }
        public abstract ReadOnlyReactiveProperty<bool> Replace { get; protected set; }
        public abstract IObservable<Unit> ChainStream { get; protected set; }
        public abstract IObservable<GridPosition> PointerGridPositionStream { get; protected set; }
        public abstract IObservable<float> CameraMovementStream { get; protected set; }
        public abstract ReadOnlyReactiveProperty<bool> MoveFaster { get; protected set; }
        public abstract IObservable<float> ScrollStream { get; protected set;  }
        public abstract IObservable<Unit> UndoStream { get; protected set;  }
        public abstract IObservable<Unit> RedoStream { get; protected set;  }
    }
}