using System;
using Remaz.Game.Map;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.UI
{
    public class TileList : MonoBehaviour
    {
        public IObservable<TileDescription> Selected { get; private set; }

        [SerializeField] private TileDatabase _tileDatabase;
        [SerializeField] private TileOption _tileOption;

        private void Awake()
        {
            Selected = new Subject<TileDescription>();
            
            foreach (TileDescription tileDescription in _tileDatabase.Tiles)
            {
                TileOption instance = Instantiate(_tileOption, transform);
                instance.DisplayTile(tileDescription);
                Selected = instance.Selected.Merge(Selected);
            }
        }
    }
}