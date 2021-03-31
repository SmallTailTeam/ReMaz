namespace ReMaz.Core.Grid.Tiles
{
    public class TileSpatial
    {
        public string Id;
        public GridPosition Position;

        public TileSpatial(string id, GridPosition position)
        {
            Id = id;
            Position = position;
        }
    }
}