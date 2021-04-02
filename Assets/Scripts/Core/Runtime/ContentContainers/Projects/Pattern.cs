using System.Collections.Generic;
using ReMaz.Core;
using ReMaz.Core.ContentContainers.Projects.Tiles;

namespace ReMaz.Core.ContentContainers.Projects
{
    public class Pattern
    {
        public GameMode Mode { get; set; }
        public List<TileSpatial> Tiles  { get; set; } =  new List<TileSpatial>();
        public int BoundLeft { get; set; }
        public int BoundRight { get; set; }
    }
}