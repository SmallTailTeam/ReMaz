using System;
using System.Collections.Generic;

namespace ReMaz.Core.Content
{
    public interface IContentContainer<T> where T : class
    {
        IObservable<T> Added { get; }
        bool HasContent { get; }
        
        void Add(T content);
        T GetRandom();
        IList<T> GetAll();
    }
}