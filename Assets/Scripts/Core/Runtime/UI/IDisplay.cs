namespace ReMaz.Core.UI
{
    public interface IDisplay<T> where T : class
    {
        T Data { get; }
        
        void Display(T data);
    }
}