namespace ReMaz.Core.UI
{
    public interface IDisplay<T>
    {
        T Content { get; }
        
        void Display(T content);
    }
}