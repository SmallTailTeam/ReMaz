namespace ReMaz.MapEditor.Commands
{
    public interface ICommand
    {
        bool Execute();
        void Undo();
    }
}