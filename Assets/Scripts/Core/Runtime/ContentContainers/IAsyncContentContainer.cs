using System;
using System.Collections;

namespace ReMaz.Core.ContentContainers
{
    public interface IAsyncContentContainer<out T> where T : class
    {
        IEnumerator GetRandomAsync(Action<T> got);
    }
}