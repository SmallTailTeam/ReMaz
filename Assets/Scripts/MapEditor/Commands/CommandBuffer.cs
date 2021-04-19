using System.Collections.Generic;
using ReMaz.MapEditor.Inputs;
using UniRx;
using UnityEngine;

namespace ReMaz.MapEditor.Commands
{
    public class CommandBuffer : MonoBehaviour
    {
        private EditorInputs _inputs;
        
        private readonly Stack<CommandChain> _undoStack = new Stack<CommandChain>();
        private readonly Stack<CommandChain> _redoStack = new Stack<CommandChain>();
        private CommandChain _chain;

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

            _inputs.ChainStream
                .Subscribe(_ => Chain())
                .AddTo(this);
        }

        public void Push(ICommand command)
        {
            bool success = command.Execute();

            if (success)
            {
                _chain ??= new CommandChain();

                _chain.Chain(command);
            }
        }

        private void Chain()
        {
            if (_chain != null)
            {
                _redoStack.Clear();
                _undoStack.Push(_chain);
                _chain = null;
            }
        }

        private void Undo()
        {
            if (_undoStack.Count <= 0)
            {
                return;
            }

            CommandChain chain = _undoStack.Pop();
            chain.UndoAll();
            _redoStack.Push(chain);
        }

        private void Redo()
        {
            if (_redoStack.Count <= 0)
            {
                return;
            }

            CommandChain chain = _redoStack.Pop();
            chain.ExecuteAll();
            _undoStack.Push(chain);
        }
    }
}