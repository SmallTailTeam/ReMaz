using System;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Inputs
{
    public class KeyboardMouseEditorInputs : EditorInputs
    {
        public override IObservable<Unit> PaintStream { get; protected set; }
        public override IObservable<Unit> EraseStream { get; protected set; }
        public override IObservable<bool> ReplaceStream { get; protected set; }
        public override IObservable<Vector3> PointerPositionStream { get; protected set; }
        public override IObservable<float> ScrollStream { get; protected set; }

        private void Start()
        {
            var updateSteam = Observable.EveryUpdate()
                .Select(_ => Unit.Default);

            PaintStream = updateSteam
                .Where(_ => Input.GetMouseButton(0));

            EraseStream = updateSteam
                .Where(_ => Input.GetMouseButton(1));

            ReplaceStream = updateSteam
                .Select(_ => Input.GetKey(KeyCode.LeftShift));

            PointerPositionStream = updateSteam
                .Select(_ => Input.mousePosition);
            
            var scrollStream = updateSteam
                .Where(_ => Input.mouseScrollDelta.y != 0f)
                .Select(_ => Input.mouseScrollDelta.y);

            ScrollStream = scrollStream.Buffer(1)
                .Select(s => s[0]);
        }
    }
}