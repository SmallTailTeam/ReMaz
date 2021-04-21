using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.UI.Selection
{
    [RequireComponent(typeof(Button))]
    public class Selectable : MonoBehaviour, ISelectable
    {
        public SelectableGroup TargetGroup => _targetGroup;
        public IObservable<ISelectable> Selected => _selected;
        public IObservable<ISelectable> Deselected => _deselected;

        [SerializeField] private SelectableGroup _targetGroup;
        [SerializeField] private bool _autoAttach;
        
        private Button _button;
        
        private ISubject<ISelectable> _selected = new Subject<ISelectable>();
        private ISubject<ISelectable> _deselected = new Subject<ISelectable>();

        private void Awake()
        {
            if (_autoAttach)
            {
                _targetGroup = GetComponentInParent<SelectableGroup>();
            }
            
            _button = GetComponent<Button>();
            
            _button.onClick.AsObservable()
                .Subscribe(_ => Select())
                .AddTo(this);

            _targetGroup.Selected
                .Subscribe(SelectionChanged)
                .AddTo(this);
            
            _targetGroup.Deselected
                .Subscribe(DeselectionChanged)
                .AddTo(this);
        }

        public void Select()
        {
            _targetGroup.Select(this);
        }

        private void SelectionChanged(ISelectable selectable)
        {
            if ((Selectable) selectable == this)
            {
                _selected.OnNext(this);
            }
        }
        
        private void DeselectionChanged(ISelectable selectable)
        {
            if ((Selectable) selectable == this)
            {
                _deselected.OnNext(this);
            }
        }
    }
}