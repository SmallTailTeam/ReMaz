using UnityEngine;

namespace ReMaz.Core.Songs
{
    public class CachedSong
    {
        public string Path;
        public AudioClip Clip;

        public CachedSong(string path)
        {
            Path = path;
        }
    }
}