using System.Collections.Generic;
using Remaz.Core.Grid.Tiles;
using ReMaz.PatternEditor.Tiles;
using ReMaz.PatternEditor.UI;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorSpace : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<TileDescription> TileToPaint {get; private set; }
        public List<TilePainted> Painted { get; private set; }
        public TilePainted SelectedTile { get; private set; }
        public bool CanPlace => _placeZone.CanPlace;
        
        [SerializeField] private PlaceZone _placeZone;
        [SerializeField] private TileList _tileList;

        private void Awake()
        {
            Painted = new List<TilePainted>();

            TileToPaint = _tileList.Selected.ToReadOnlyReactiveProperty();
        }
    }
}