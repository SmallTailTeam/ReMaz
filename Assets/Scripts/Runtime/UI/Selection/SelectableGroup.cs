using System;
using UniRx;
using UnityEngine;

namespace ReMaz.UI.Selection
{
    public class SelectableGroup : MonoBehaviour
    {
        public IObservable<ISelectable> Selected => _selected;
        public IObservable<ISelectable> Deselected => _deselected;

        private ISubject<ISelectable> _selected = new Subject<ISelectable>();
        private ISubject<ISelectable> _deselected = new Subject<ISelectable>();
        private ISelectable _selection;

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

        public void Deselect()
        {
            if (_selection != null)
            {
                _deselected.OnNext(_selection);
                _selection = null;
            }
        }
    }
}