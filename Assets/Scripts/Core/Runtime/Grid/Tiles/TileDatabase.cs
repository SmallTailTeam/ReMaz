using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReMaz.Core.ContentContainers.Projects.Tiles
{
    [CreateAssetMenu(menuName = "Grid/Tile database", fileName = "TileDatabase")]
    public class TileDatabase : ScriptableObject
    {
        public List<TileDescription> Tiles => _tiles;
        
        [SerializeField] private List<TileDescription> _tiles;

        public TileDescription FindTile(string tileId)
        {
            return _tiles.FirstOrDefault(tile => tile.Id == tileId);
        }
    }
}