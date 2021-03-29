using System;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Inputs
{
    public abstract class EditorInputs : MonoBehaviour
    {
        public abstract IObservable<Unit> PaintStream { get; protected set; }
        public abstract IObservable<Unit> EraseStream { get; protected set; }
        public abstract IObservable<bool> ReplaceStream { get; protected set; }
        public abstract IObservable<Vector3> PointerPositionStream { get; protected set; }
        public abstract IObservable<float> ScrollStream { get; protected set;  }
    }
}