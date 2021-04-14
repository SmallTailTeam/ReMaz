using UnityEngine;

namespace ReMaz.Core.Content.Projects.Tiles
{
    public class TileColor
    {
        public int r;
        public int g;
        public int b;

        public TileColor(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Color ToColor()
        {
            return new Color(r / 255f, g / 255f, b / 255f, 1f);
        }

        public static TileColor FromColor(Color color)
        {
            return new TileColor(Mathf.RoundToInt(color.r * 255f), Mathf.RoundToInt(color.g * 255f), Mathf.RoundToInt(color.b * 255f));
        }
    }
}