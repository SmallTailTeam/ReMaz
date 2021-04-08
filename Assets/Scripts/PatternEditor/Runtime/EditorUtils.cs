using System.Linq;
using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.PatternEditor.Tiles;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public static class EditorUtils
    {
        public static void CreateInstance(GridPosition gridPosition, TileDescription tileToPaint, EditorSpace editorSpace)
        {
            GameObject instance = Object.Instantiate(tileToPaint.Prefab, editorSpace.transform);
            instance.transform.position = gridPosition.ToWorld();
            instance.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, tileToPaint.Rotation));

            TilePainted tilePainted = new TilePainted(tileToPaint.Id, instance, gridPosition)
            {
                Graphics = instance.GetComponentInChildren<SpriteRenderer>()
            };
            
            editorSpace.Painted.Add(tilePainted);
        }

        public static bool Paint(GridPosition gridPosition, EditorSpace editorSpace, TileDescription tileToPaint)
        {
            TilePainted existentTile = editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));

            if (existentTile == null)
            {
                CreateInstance(gridPosition, tileToPaint, editorSpace);

                TileSpatial tileSpatial = new TileSpatial(tileToPaint.Id, gridPosition);
                EditorProject.CurrentProject.Content.Tiles.Add(tileSpatial);

                return true;
            }

            return false;
        }

        public static bool Erase(GridPosition gridPosition, EditorSpace editorSpace)
        {
            TilePainted tilePainted = editorSpace.Painted.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
            
            if (tilePainted != null)
            {
                Object.Destroy(tilePainted.Instance);
                editorSpace.Painted.Remove(tilePainted);

                TileSpatial tileSpatial = EditorProject.CurrentProject.Content.Tiles.FirstOrDefault(tile => tile.Position.Overlap(gridPosition));
                EditorProject.CurrentProject.Content.Tiles.Remove(tileSpatial);

                return true;
            }

            return false;
        }
    }
}