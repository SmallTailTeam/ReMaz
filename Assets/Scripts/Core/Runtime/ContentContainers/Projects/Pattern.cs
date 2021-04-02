using System.Collections.Generic;
using ReMaz.Core.ContentContainers.Projects.Tiles;

namespace ReMaz.Core.ContentContainers.Projects
{
    public class Pattern
    {
        public List<TileSpatial> Tiles = new List<TileSpatial>();
        public int BoundLeft;
        public int BoundRight;
    }
}