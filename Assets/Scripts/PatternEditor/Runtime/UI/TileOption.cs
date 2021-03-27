using System;
using Remaz.Game.Map;
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
        public TileDescription DisplayedTile => _dispalyedTile;
        
        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private Image _image;
        [SerializeField, AutoHook] private Button _button;

        private Subject<TileDescription> _selected;
        private TileDescription _dispalyedTile;
        
        private void Awake()
        {
            _selected = new Subject<TileDescription>();
        }

        private void Start()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => _selected?.OnNext(_dispalyedTile))
                .AddTo(this);
        }

        public void DisplayTile(TileDescription tileDescription)
        {
            _dispalyedTile = tileDescription;
            
            _image.sprite = tileDescription.Icon;
        }
    }
}