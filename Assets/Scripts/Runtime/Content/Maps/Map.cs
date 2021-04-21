using System.Collections.Generic;
using ReMaz.Grid.Tiles;

namespace ReMaz.Grid.Maps
{
    public class Map
    {
        public string Id { get; set; }
        public string SongId { get; set; }
        public string Name { get; set; }
        public List<TileSpatial> Tiles  { get; } = new List<TileSpatial>();
    }
}