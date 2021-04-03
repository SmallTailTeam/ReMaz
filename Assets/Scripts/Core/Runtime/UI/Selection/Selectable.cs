﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.Core.UI.Selection
{
    [RequireComponent(typeof(Button))]
    public class Selectable : MonoBehaviour, ISelectable
    {
        public SelectableGroup TargetGroup => _targetGroup;
        public IObservable<ISelectable> Selected => _selected;
        public IObservable<ISelectable> Deselected => _deselected;

        [SerializeField] private SelectableGroup _targetGroup;
        
        private Button _button;
        
        private ISubject<ISelectable> _selected;
        private ISubject<ISelectable> _deselected;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _selected = new Subject<ISelectable>();
            _deselected = new Subject<ISelectable>();
        }

        private void Start()
        {
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