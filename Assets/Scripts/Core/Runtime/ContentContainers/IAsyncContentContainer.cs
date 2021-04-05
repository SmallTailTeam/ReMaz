using System;

namespace ReMaz.Core.ContentContainers
{
    public interface IAsyncContentContainer<T> : IContentContainer<T> where T : class
    {
        IObservable<T> GetRandomAsync();
        IObservable<T>  GetAsync(T content);
    }
}