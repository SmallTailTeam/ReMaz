using System.Collections.Generic;

namespace ReMaz.MapEditor.Commands
{
    public class CommandChain
    {
        private List<ICommand> _commands = new List<ICommand>();

        public void Chain(ICommand command)
        {
            _commands.Add(command);
        }

        public void ExecuteAll()
        {
            foreach (ICommand command in _commands)
            {
                command.Execute();
            }
        }
        
        public void UndoAll()
        {
            foreach (ICommand command in _commands)
            {
                command.Undo();
            }
        }
    }
}