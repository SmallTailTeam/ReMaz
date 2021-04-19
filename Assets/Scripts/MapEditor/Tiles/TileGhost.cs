using UnityEngine;

namespace ReMaz.MapEditor.Tiles
{
    public class TileGhost : MonoBehaviour
    {
        public SpriteRenderer Graphics { get; private set; }

        private void Awake()
        {
            Graphics = GetComponentInChildren<SpriteRenderer>();
        }
    }
}