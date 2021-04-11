using System;

namespace ReMaz.PatternEditor
{
    public interface IProjectEditor<T>
    {
        IObservable<T> ProjectLoaded { get; }
        
        T Project { get; }

        void Open(T project);
        void Create();
        void Save();
    }
}