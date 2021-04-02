namespace ReMaz.Core.ContentContainers.Projects.Tiles
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