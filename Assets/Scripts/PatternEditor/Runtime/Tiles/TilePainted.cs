using ReMaz.Core.ContentContainers.Projects;
using UnityEngine;

namespace ReMaz.PatternEditor.Tiles
{
    public class TilePainted
    {
        public string Id;
        public GameObject Instance;
        public SpriteRenderer Graphics;
        public GridPosition Position;

        public TilePainted(string id, GameObject instance, GridPosition position)
        {
            Id = id;
            Instance = instance;
            Position = position;
        }
    }
}