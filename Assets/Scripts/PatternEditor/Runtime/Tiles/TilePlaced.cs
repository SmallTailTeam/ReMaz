using Remaz.Game.Grid;
using UnityEngine;

namespace ReMaz.PatternEditor.Tiles
{
    public class TilePlaced
    {
        public string Id;
        public GameObject Instance;
        public GridPosition Position;

        public TilePlaced(string id, GameObject instance, GridPosition position)
        {
            Id = id;
            Instance = instance;
            Position = position;
        }
    }
}