namespace ReMaz.UI
{
    public interface IDisplay<T>
    {
        T Content { get; }
        
        void Display(T content);
    }
}