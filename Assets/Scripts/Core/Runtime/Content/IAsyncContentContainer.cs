using System;

namespace ReMaz.Core.Content
{
    public interface IAsyncContentContainer<T> : IContentContainer<T> where T : class
    {
        IObservable<T> GetRandomAsync();
        IObservable<T>  GetAsync(T content);
    }
}