namespace ReMaz.PatternEditor.Commands
{
    public interface ICommand
    {
        bool Execute();
        void Undo();
    }
}