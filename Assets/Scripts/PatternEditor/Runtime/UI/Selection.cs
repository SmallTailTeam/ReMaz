using Remaz.Game.Grid.Tiles;
using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.PatternEditor.UI
{
    public class Selection : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _graphics;
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] private TileOption _tileOption;
        [Header("Colors")]
        [SerializeField] private Color _restColor;
        [SerializeField] private Color _selectedColor;

        private TileList _tileList;

        private void Awake()
        {
            _tileList = GetComponentInParent<TileList>();
        }

        private void Start()
        {
            _tileList.Selected
                .Where(x => x == _tileOption.DisplayedTile)
                .Subscribe(Selected)
                .AddTo(this);
            
            _tileList.Selected
                .Where(x => x != _tileOption.DisplayedTile)
                .Subscribe(NotSelected)
                .AddTo(this);
        }

        private void Selected(TileDescription _)
        {
            _graphics.color = _selectedColor;
        }

        private void NotSelected(TileDescription _)
        {
            _graphics.color = _restColor;
        }
    }
}