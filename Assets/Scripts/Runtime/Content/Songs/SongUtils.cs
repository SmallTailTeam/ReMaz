using ReMaz.Content.Songs.Reading;
using UnityEngine;

namespace ReMaz.Content.Songs
{
    public static class SongUtils
    {
        public static AudioClip AssembleClip(SongPCM data)
        {
            AudioClip clip = AudioClip.Create("test", data.Samples, data.Channels, data.Frequency, false);
            clip.SetData(data.Wave, 0);
            return clip;
        }
    }
}