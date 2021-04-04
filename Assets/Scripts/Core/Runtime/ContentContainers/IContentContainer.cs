using System.Collections.Generic;

namespace ReMaz.Core.ContentContainers
{
    public interface IContentContainer<T> where T : class
    {
        void Add(T content);
        T GetRandom();
        IList<T> GetAll();
    }
}