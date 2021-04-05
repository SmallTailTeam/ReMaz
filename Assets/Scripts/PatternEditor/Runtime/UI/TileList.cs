using System;
using System.Collections.Generic;
using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.PatternEditor.Inputs;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ReMaz.PatternEditor.UI
{
    public class TileList : MonoBehaviour
    {
        public IObservable<TileDescription> Selected { get; private set; }
        public TileDatabase TileDatabase => _tileDatabase;

        [SerializeField] private TileDatabase _tileDatabase;
        [SerializeField] private TileOption _tileOption;

        private EditorInputs _inputs;
        
        private List<TileOption> _options;
        private int _selectionIndex;

        private void Awake()
        {
            _inputs = FindObjectOfType<EditorInputs>();
            
            Selected = new Subject<TileDescription>();
            _options = new List<TileOption>();

            foreach (TileDescription tileDescription in _tileDatabase.Tiles)
            {
                InstantiateOption(tileDescription);
            }
        }
        
        private void Start()
        {
            _inputs.ScrollStream
                .Subscribe(Scroll)
                .AddTo(this);
            
            if (_options.Count > 0)
            {
                _options[0].Select();
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