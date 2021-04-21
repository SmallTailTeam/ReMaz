using System;
using ReMaz.Grid.Tiles;
using ReMaz.UI.Selection;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.MapEditor.UI
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
            for (int i = _tileDatabase.Tiles.Count - 1; i >= 0; i--)
            {
                TileDisplay tileDisplay = InstantiateOption(_tileDatabase.Tiles[i]);

                if (i == 0)
                {
                    tileDisplay.gameObject.AddComponent<DefaultSelection>();
                }
            }

            _selectableGroup.Selected
                .Subscribe(TileSelected)
                .AddTo(this);
        }

        private TileDisplay InstantiateOption(TileDescription tileDescription)
        {
            TileDisplay instance = Instantiate(_tileDisplayPrefab, transform);
            
            instance.Display(tileDescription);

            return instance;
        }

        private void TileSelected(ISelectable selectable)
        {
            if (selectable is ISelectableDisplay<TileDescription> tileDisplay)
            {
                _selected.OnNext(tileDisplay.Content);
            }
            else
            {
                _selected.OnNext(null);
            }
        }
    }
}