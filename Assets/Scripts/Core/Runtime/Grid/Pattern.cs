using System.Collections.Generic;
using Remaz.Core.Grid.Tiles;

namespace Remaz.Core.Grid
{
    public class Pattern
    {
        public List<TileSpatial> Tiles = new List<TileSpatial>();
        public int BoundLeft;
        public int BoundRight;
    }
}