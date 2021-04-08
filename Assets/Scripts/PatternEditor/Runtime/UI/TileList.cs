using System;
using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.Core.UI.Selection;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.UI
{
    public class TileList : MonoBehaviour
    {
        public IObservable<TileDescription> Selected => _selected;
        public TileDatabase TileDatabase => _tileDatabase;

        [SerializeField, AutoHook] private SelectableGroup _selectableGroup;
        [SerializeField] private TileDatabase _tileDatabase;
        [SerializeField] private TileDisplay _tileDisplayPrefab;

        private ISubject<TileDescription> _selected;
        
        private void Awake()
        {
            _selected = new Subject<TileDescription>();
        }

        private void Start()
        {
            foreach (TileDescription tileDescription in _tileDatabase.Tiles)
            {
                InstantiateOption(tileDescription);
            }
            
            _selectableGroup.Selected
                .Subscribe(TileSelected)
                .AddTo(this);
        }

        private void InstantiateOption(TileDescription tileDescription)
        {
            TileDisplay instance = Instantiate(_tileDisplayPrefab, transform);
            
            instance.Display(tileDescription);
        }

        private void TileSelected(ISelectable selectable)
        {
            if (selectable is ISelectableDisplay<TileDescription> tileDisplay)
            {
                _selected.OnNext(tileDisplay.Content);
            }
        }
    }
}