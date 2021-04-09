using System;
using System.Linq;
using ReMaz.Core.Content.Projects;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Inputs
{
    public class KeyboardMouseEditorInputs : EditorInputs
    {
        public override IObservable<Unit> PaintStream { get; protected set; }
        public override IObservable<Unit> EraseStream { get; protected set; }
        public override ReadOnlyReactiveProperty<bool> Replace { get; protected set; }
        public override IObservable<Unit> ChainStream { get; protected set; }
        public override IObservable<GridPosition> PointerPositionStream { get; protected set; }
        public override IObservable<float> ScrollStream { get; protected set; }
        public override IObservable<Unit> UndoStream { get; protected set; }
        public override IObservable<Unit> RedoStream { get; protected set; }

#if UNITY_EDITOR
        private KeyCode[] _undoCombination = { KeyCode.LeftAlt, KeyCode.Z };
        private KeyCode[] _redoCombination = { KeyCode.LeftAlt, KeyCode.LeftShift, KeyCode.Z };
#else
        private KeyCode[] _undoCombination = { KeyCode.LeftControl, KeyCode.Z };
        private KeyCode[] _redoCombination = { KeyCode.LeftControl, KeyCode.LeftShift, KeyCode.Z };
#endif

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

            ChainStream = updateSteam
                .Where(_ => Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftShift));
            
            PointerPositionStream = updateSteam
                .Select(_ => GridPosition.FromWorld(Camera.main.ScreenToWorldPoint(Input.mousePosition)));

            var scrollStream = updateSteam
                .Where(_ => Input.mouseScrollDelta.y != 0f)
                .Select(_ => Input.mouseScrollDelta.y);

            ScrollStream = scrollStream.Buffer(1)
                .Select(s => s[0]);

#if UNITY_EDITOR
                var controlStream = updateSteam
                .Where(_ => Input.GetKeyDown(KeyCode.LeftAlt))
                .Select(_ => KeyCode.LeftAlt);
#else
                var controlStream = updateSteam
                .Where(_ => Input.GetKeyDown(KeyCode.LeftControl))
                .Select(_ => KeyCode.LeftControl);
#endif

            var shiftStream = updateSteam
                .Where(_ => Input.GetKeyDown(KeyCode.LeftShift))
                .Select(_ => KeyCode.LeftShift);
            
            var zStream = updateSteam
                .Where(_ => Input.GetKeyDown(KeyCode.Z))
                .Select(_ => KeyCode.Z);

            var undoRedoInputStream = Observable.Merge(controlStream, shiftStream, zStream);

            UndoStream = undoRedoInputStream
                .Buffer(undoRedoInputStream.Throttle(TimeSpan.FromMilliseconds(250)))
                .Where(keys => keys.SequenceEqual(_undoCombination) && !keys.Contains(KeyCode.LeftShift))
                .Select(_ => Unit.Default);

            RedoStream = undoRedoInputStream
                .Buffer(undoRedoInputStream.Throttle(TimeSpan.FromMilliseconds(250)))
                .Where(keys => keys.SequenceEqual(_redoCombination))
                .Select(_ => Unit.Default);
        }
    }
}