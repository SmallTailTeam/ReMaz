using System.Collections.Generic;
using ReMaz.Core.Content.Projects.Patterns;
using ReMaz.Core.Content.Projects.Tiles;
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
        public bool CanPlace => _placeZone.CanPlace;
        public TileDatabase TileDatabase => _tileList.TileDatabase;
        
        [SerializeField] private PlaceZone _placeZone;
        [SerializeField] private TileList _tileList;

        private void Start()
        {
            Painted = new List<TilePainted>();

            TileToPaint = _tileList.Selected.ToReadOnlyReactiveProperty();
        }
    }
}