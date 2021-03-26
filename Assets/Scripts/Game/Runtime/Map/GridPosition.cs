using UnityEngine;

namespace Remaz.Game.Map
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

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, 0f);
        }
        
        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
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