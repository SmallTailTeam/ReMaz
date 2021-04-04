using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmallTail.Preload.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ReMaz.Core.ContentContainers.Songs
{
    [Preloaded]
    public class Playlist : MonoBehaviour, IAsyncContentContainer<Song>
    {
        private List<Song> _cachedSongs = new List<Song>();

        private void Awake()
        {
            IndexSongs();
        }

        private void OnApplicationQuit()
        {
            BassManager.Free();
        }

        private void IndexSongs()
        {
            if (!Directory.Exists(ContentFileSystem.SongsPath))
            {
                return;
            }

            DirectoryInfo directory = new DirectoryInfo(ContentFileSystem.SongsPath);
            
            foreach (DirectoryInfo songDirectory in directory.GetDirectories())
            {
                try
                {
                    string songFile = songDirectory.FullName + @"\Song.file";
                    string songMetaFile = songDirectory.FullName + @"\Meta.file";

                    string songMetaJson = File.ReadAllText(songMetaFile);
                    SongMeta songMeta = JsonConvert.DeserializeObject<SongMeta>(songMetaJson);
                    
                    Song song = new Song(songFile)
                    {
                        Meta = songMeta
                    };

                    _cachedSongs.Add(song);
                }
                catch
                {
                    // ignore, i guess
                }
            }
        }

        public void Process(string path)
        {
            AudioClip clip = BassManager.Open(path);

            string id = Guid.NewGuid().ToString();
            string songPath = ContentFileSystem.SongsPath + $"/{id}";
            Directory.CreateDirectory(songPath);

            File.Copy(path, songPath + "/Song.file");
            
            SongMeta songMeta = new SongMeta
            {
                Name = Path.GetFileNameWithoutExtension(path),
                Length = clip.length
            };

            string songMetaJson = JsonConvert.SerializeObject(songMeta);
            File.WriteAllText(songPath + "/Meta.file", songMetaJson);

            Song song = new Song(songPath + "/Song.file")
            {
                Meta = songMeta,
                Clip = clip
            };
            
            _cachedSongs.Add(song);
        }
        
        public void Add(Song content)
        {
            _cachedSongs.Add(content);
        }
        
        public IEnumerator GetRandomAsync(Action<Song> got)
        {
            Song song = GetRandom();
            
            if (song.Clip == null)
            {
                song.Clip = BassManager.Open(song.Path);
            }

            got?.Invoke(song);
            yield return null;
        }

        public Song GetRandom()
        {
            Song song = _cachedSongs[Random.Range(0, _cachedSongs.Count)];
            return song;
        }

        public IList<Song> GetAll()
        {
            return _cachedSongs;
        }
    }
}