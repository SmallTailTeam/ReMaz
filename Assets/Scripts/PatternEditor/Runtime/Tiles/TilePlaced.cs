using Remaz.Game.Grid;
using UnityEngine;

namespace ReMaz.PatternEditor.Tiles
{
    public class TilePlaced
    {
        public GameObject Instance;
        public GridPosition Position;

        public TilePlaced(GameObject instance, GridPosition position)
        {
            Instance = instance;
            Position = position;
        }
    }
}