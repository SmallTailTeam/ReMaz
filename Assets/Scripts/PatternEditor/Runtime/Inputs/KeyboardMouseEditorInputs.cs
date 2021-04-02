using System;
using ReMaz.Core.ContentContainers.Projects;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Inputs
{
    public class KeyboardMouseEditorInputs : EditorInputs
    {
        public override IObservable<Unit> PaintStream { get; protected set; }
        public override IObservable<Unit> EraseStream { get; protected set; }
        public override ReadOnlyReactiveProperty<bool> Replace { get; protected set; }
        public override IObservable<GridPosition> PointerPositionStream { get; protected set; }
        public override IObservable<float> ScrollStream { get; protected set; }

        private void Awake()
        {
            var updateSteam = Observable.EveryUpdate()
                .Select(_ => Unit.Default);

            PaintStream = updateSteam
                .Where(_ => Input.GetMouseButton(0));

            EraseStream = updateSteam
                .Where(_ => Input.GetMouseButton(1));

            Replace = updateSteam
                .Select(_ => Input.GetKey(KeyCode.LeftShift))
                .ToReadOnlyReactiveProperty();

            PointerPositionStream = updateSteam
                .Select(_ => GridPosition.FromWorld(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            
            var scrollStream = updateSteam
                .Where(_ => Input.mouseScrollDelta.y != 0f)
                .Select(_ => Input.mouseScrollDelta.y);

            ScrollStream = scrollStream.Buffer(1)
                .Select(s => s[0]);
        }
    }
}