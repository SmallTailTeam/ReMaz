using System;

namespace ReMaz.Core.UI.Selection
{
    public interface ISelectable
    {
        IObservable<ISelectable> Selected { get; }
        IObservable<ISelectable> Deselected { get; }
    }
}