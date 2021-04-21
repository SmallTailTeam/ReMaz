using System;
using System.Collections.Generic;

namespace ReMaz.Content
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