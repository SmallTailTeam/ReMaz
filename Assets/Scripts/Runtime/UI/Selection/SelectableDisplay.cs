namespace ReMaz.UI.Selection
{
    public abstract class SelectableDisplay<T> : Selectable, ISelectableDisplay<T>
    {
        public T Content { get; protected set; }

        public abstract void Display(T content);
    }
}