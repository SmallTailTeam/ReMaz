using System.Collections.Generic;
using ReMaz.Core;
using ReMaz.Core.Content.Projects.Tiles;

namespace ReMaz.Core.Content.Projects.Patterns
{
    public class Pattern
    {
        public GameMode Mode { get; set; }
        public List<TileSpatial> Tiles  { get; set; } =  new List<TileSpatial>();
        public int BoundLeft { get; set; }
        public int BoundRight { get; set; }
    }
}