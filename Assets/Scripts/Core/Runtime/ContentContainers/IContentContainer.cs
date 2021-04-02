namespace ReMaz.Core.ContentContainers
{
    public interface IContentContainer<out T> where T : class
    {
        T GetRandom();
    }
}