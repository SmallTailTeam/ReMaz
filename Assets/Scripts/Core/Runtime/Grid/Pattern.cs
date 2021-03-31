using System.Collections.Generic;
using ReMaz.Core.Grid.Tiles;

namespace ReMaz.Core.Grid
{
    public class Pattern
    {
        public List<TileSpatial> Tiles = new List<TileSpatial>();
        public int BoundLeft;
        public int BoundRight;
    }
}