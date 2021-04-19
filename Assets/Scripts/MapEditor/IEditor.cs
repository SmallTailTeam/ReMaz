using System;

namespace ReMaz.MapEditor
{
    public interface IEditor<T>
    {
        IObservable<T> ProjectLoaded { get; }
        
        T Project { get; }

        void Open(T project);
        void Create();
        void Save();
    }
}