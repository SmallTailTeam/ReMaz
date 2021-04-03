using System.Collections.Generic;

namespace ReMaz.Core.ContentContainers
{
    public interface IContentContainer<T> where T : class
    {
        T GetRandom();
        IList<T> GetAll();
    }
}