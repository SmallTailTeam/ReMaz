using System;
using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.Selection
{
    public class SelectableGroup : MonoBehaviour, ISelectable
    {
        public IObservable<ISelectable> Selected => _selected;
        public IObservable<ISelectable> Deselected => _deselected;
        
        private ISubject<ISelectable> _selected;
        private ISubject<ISelectable> _deselected;
        private ISelectable _selection;
        
        private void Awake()
        {
            _selected = new Subject<ISelectable>();
            _deselected = new Subject<ISelectable>();
        }

        public void Select(ISelectable selectable)
        {
            if (_selection != selectable)
            {
                if (_selection != null)
                {
                    _deselected.OnNext(_selection);
                }

                _selection = selectable;
                _selected.OnNext(_selection);
            }
        }
    }
}