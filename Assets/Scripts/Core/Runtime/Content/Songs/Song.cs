using System.IO;
using Newtonsoft.Json;
using ReMaz.Core.Content.Songs.Analyzing;
using ReMaz.Core.Content.Songs.Reading;
using UnityEngine;

namespace ReMaz.Core.Content.Songs
{
    public class Song
    {
        public string Path;
        public AudioClip Clip;
        public SongMeta Meta;
        public SongSpectrum Spectrum;
        public SongPCM PCM;

        private const string _songFile = "Song.file";
        private const string _metaFile = "Meta.file";
        private const string _spectrumFile = "Spectrum.file";
        
        public Song(string path)
        {
            Path = path;
        }

        public void Serialize(string originalSongFile)
        {
            File.Copy(originalSongFile, Path + $"/{_songFile}");
            
            string metaJson = JsonConvert.SerializeObject(Meta);
            File.WriteAllText(Path + $"/{_metaFile}", metaJson);
            
            string spectrumJson = JsonConvert.SerializeObject(Spectrum);
            File.WriteAllText(Path + $"/{_spectrumFile}", spectrumJson);
        }

        public static Song Deserialize(string path)
        {
            string metaJson = File.ReadAllText(path + $"/{_metaFile}");
            SongMeta meta = JsonConvert.DeserializeObject<SongMeta>(metaJson);
            
            string spectrumJson = File.ReadAllText(path + $"/{_spectrumFile}");
            SongSpectrum spectrum = JsonConvert.DeserializeObject<SongSpectrum>(spectrumJson);

            Song song = new Song(path)
            {
                Path = path + $"/{_songFile}",
                Meta = meta,
                Spectrum = spectrum
            };

            return song;
        }
    }
}