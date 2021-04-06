namespace ReMaz.Core.Content.Projects
{
    public interface IProject<T>
    {
        string Id { get; }
        string Name { get; }
        T Content { get; }

        void Rename(string name);
    }
}