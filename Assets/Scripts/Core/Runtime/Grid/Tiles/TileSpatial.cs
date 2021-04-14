namespace ReMaz.Core.Content.Projects.Tiles
{
    public class TileSpatial
    {
        public string Id;
        public GridPosition Position;
        public TileColor Color;

        public TileSpatial(string id, GridPosition position, TileColor color)
        {
            Id = id;
            Position = position;
            Color = color;
        }
    }
}