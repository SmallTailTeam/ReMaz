using System;
using System.Collections.Generic;
using Remaz.Game.Grid.Tiles;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.UI
{
    public class TileList : MonoBehaviour
    {
        public IObservable<TileDescription> Selected { get; private set; }

        [SerializeField] private TileDatabase _tileDatabase;
        [SerializeField] private TileOption _tileOption;

        private List<TileOption> _options;
        private int _selectionIndex;

        private void Awake()
        {
            Selected = new Subject<TileDescription>();
            _options = new List<TileOption>();

            foreach (TileDescription tileDescription in _tileDatabase.Tiles)
            {
                InstantiateOption(tileDescription);
            }
        }

        private void InstantiateOption(TileDescription tileDescription)
        {
            TileOption instance = Instantiate(_tileOption, transform);
            
            instance.DisplayTile(tileDescription);
            
            int index = _options.Count;
            instance.Selected.Subscribe(_ => _selectionIndex = index)
                .AddTo(this);
            
            Selected = instance.Selected.Merge(Selected);

            _options.Add(instance);
        }
        
        private void Start()
        {
            var scrollStream = Observable.EveryUpdate()
                .Where(_ => Input.mouseScrollDelta.y != 0f)
                .Select(_ => Input.mouseScrollDelta.y);

            scrollStream.Buffer(1)
                .Select(s => s[0])
                .Subscribe(Scroll)
                .AddTo(this);
        }

        private void Scroll(float direction)
        {
            int nextIndex = direction > 0f ? _selectionIndex + 1 : _selectionIndex - 1;
            
            nextIndex = nextIndex < _options.Count ? nextIndex : 0;
            nextIndex = nextIndex < 0 ? _options.Count - 1 : nextIndex;

            TileOption nextOption = _options[nextIndex];

            nextOption.Select();
        }
    }
}