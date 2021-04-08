using System.Collections.Generic;
using ReMaz.PatternEditor.Inputs;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Commands
{
    public class CommandBuffer : MonoBehaviour
    {
        private EditorInputs _inputs;
        
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

        private void Awake()
        {
            _inputs = FindObjectOfType<EditorInputs>();
        }

        private void Start()
        {
            _inputs.UndoStream
                .Subscribe(_ => Undo())
                .AddTo(this);
            
            _inputs.RedoStream
                .Subscribe(_ => Redo())
                .AddTo(this);
        }

        public void Push(ICommand command)
        {
            bool success = command.Execute();

            if (success)
            {
                _redoStack.Clear();
                _undoStack.Push(command);
            }
        }

        private void Undo()
        {
            if (_undoStack.Count <= 0)
            {
                return;
            }

            ICommand command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }

        private void Redo()
        {
            if (_redoStack.Count <= 0)
            {
                return;
            }

            ICommand command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
        }
    }
}