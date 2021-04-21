using UnityEngine;

namespace ReMaz.Grid
{
    public struct GridPosition
    {
        public int x;
        public int y;

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector3 ToWorld()
        {
            return new Vector3(x - ScreenGrid.Size.Value.x * 0.5f, y - ScreenGrid.Size.Value.y * 0.5f, 0f);
        }

        public static GridPosition FromWorld(Vector3 worldPos)
        {
            Vector2 halfSize = ScreenGrid.Size.Value * 0.5f;
            
            int x = Mathf.RoundToInt(Mathf.Max(0f, worldPos.x + halfSize.x));
            int y = Mathf.RoundToInt(Mathf.Clamp(worldPos.y + halfSize.y, 0, ScreenGrid.Size.Value.y));
            
            return new GridPosition(x, y);
        }

        public bool Overlap(GridPosition gridPosition)
        {
            return x == gridPosition.x && y == gridPosition.y;
        }

        public override string ToString()
        {
            return $"[{x}, {y}]";
        }
    }
}