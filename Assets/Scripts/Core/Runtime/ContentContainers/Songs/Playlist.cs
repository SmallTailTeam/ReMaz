using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using SmallTail.Preload.Attributes;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ReMaz.Core.ContentContainers.Songs
{
    [Preloaded]
    public class Playlist : MonoBehaviour, IAsyncContentContainer<Song>
    {
        public IObservable<Song> Added => _added;
        
        private List<Song> _cachedSongs = new List<Song>();
        private ISubject<Song> _added;

        private void Awake()
        {
            _added = new Subject<Song>();
            
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

        private void AddFromData(AudioData data, string path)
        {
            string id = Guid.NewGuid().ToString();
            string songPath = ContentFileSystem.SongsPath + $"/{id}";
            Directory.CreateDirectory(songPath);

            SongMeta songMeta = new SongMeta
            {
                Name = Path.GetFileNameWithoutExtension(path),
                Length = (float)data.Samples / data.Frequency
            };

            Song song = new Song(songPath + "/Song.file")
            {
                Meta = songMeta,
                Data = data
            };

            File.Copy(path, songPath + "/Song.file");
            
            string songMetaJson = JsonConvert.SerializeObject(songMeta);
            File.WriteAllText(songPath + "/Meta.file", songMetaJson);
            
            Add(song);
        }
        
        public UniTask Process(string path)
        {
            AudioData data = BassManager.Open(path);
            AddFromData(data, path);
            
            return UniTask.CompletedTask;
        }

        public void Add(Song content)
        {
            _cachedSongs.Add(content);
            _added.OnNext(content);
        }
        
        public IObservable<Song> GetRandomAsync()
        {
            Song song = GetRandom();

            return GetAsync(song);
        }

        public IObservable<Song> GetAsync(Song content)
        {
            if (content != null && content.Clip == null)
            {
                if (content.Data == null)
                {
                    return Observable.Start(() =>
                        {
                            AudioData data = BassManager.Open(content.Path);
                            return data;
                        })
                        .ObserveOnMainThread()
                        .Select(data =>
                        {
                            content.Data = data;
                            content.Clip = BassManager.AssembleClip(content.Data);
                            content.Data = null;
                            return content;
                        });
                }
                
                content.Clip = BassManager.AssembleClip(content.Data);
                content.Data = null;
            }
            
            return Observable.Start(() => content)
                .ObserveOnMainThread();
        }
        
        public Song GetRandom()
        {
            if (_cachedSongs.Count < 1)
            {
                return null;
            }

            Song song = _cachedSongs[Random.Range(0, _cachedSongs.Count)];
            return song;
        }
        
        public IList<Song> GetAll()
        {
            return _cachedSongs;
        }
    }
}