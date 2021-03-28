using System;
using Remaz.Game.Grid.Tiles;
using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.PatternEditor.UI
{
    [RequireComponent(typeof(Button))]
    public class TileOption : MonoBehaviour
    {
        public IObservable<TileDescription> Selected => _selected;
        public TileDescription DisplayedTile { get; private set; }

        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private Image _image;
        [SerializeField, AutoHook] private Button _button;

        private Subject<TileDescription> _selected;

        private void Awake()
        {
            _selected = new Subject<TileDescription>();
        }

        private void Start()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => _selected?.OnNext(DisplayedTile))
                .AddTo(this);
        }

        public void DisplayTile(TileDescription tileDescription)
        {
            DisplayedTile = tileDescription;
            
            _image.sprite = tileDescription.Icon;
        }
    }
}