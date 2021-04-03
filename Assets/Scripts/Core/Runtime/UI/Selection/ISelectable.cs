using System;

namespace ReMaz.Core.UI.Selection
{
    public interface ISelectable
    {
        SelectableGroup TargetGroup { get; }
        
        IObservable<ISelectable> Selected { get; }
        IObservable<ISelectable> Deselected { get; }

        void Select();
    }
}