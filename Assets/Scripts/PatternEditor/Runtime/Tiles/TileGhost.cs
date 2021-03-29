using UnityEngine;

namespace ReMaz.PatternEditor.Tiles
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