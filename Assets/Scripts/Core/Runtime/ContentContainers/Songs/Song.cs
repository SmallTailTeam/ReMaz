﻿using UnityEngine;

namespace ReMaz.Core.ContentContainers.Songs
{
    public class Song
    {
        public string Path;
        public SongMeta Meta;
        public AudioData Data;
        public AudioClip Clip;

        public Song(string path)
        {
            Path = path;
        }
    }
}