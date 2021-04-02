using UnityEngine;

namespace ReMaz.Core.ContentContainers.Songs
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