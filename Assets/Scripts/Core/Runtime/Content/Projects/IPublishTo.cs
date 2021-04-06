namespace ReMaz.Core.Content.Projects
{
    public interface IPublishTo<T>
    {
        T PublishData { get; }

        void Publish(T publishData);
    }
}