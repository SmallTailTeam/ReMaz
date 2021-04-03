namespace ReMaz.Core.UI
{
    public interface IDisplay<T> where T : class
    {
        void Display(T data);
    }
}