using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using ReMaz.Content.Songs.Analyzing;
using ReMaz.Content.Songs.Reading;
using ReMaz.Content;
using SmallTail.Preload.Attributes;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ReMaz.Content.Songs
{
    [Preloaded]
    public class Playlist : MonoBehaviour, IAsyncContentContainer<Song>
    {
        public IObservable<Song> Added => _added;
        public bool HasContent { get; private set; }

        private ISongReader _songReader;
        private ISongAnalyzer _songAnalyzer;
        private List<Song> _cachedSongs = new List<Song>();
        private ISubject<Song> _added;

        private void Awake()
        {
            _songReader = new BassSongReader();
            _songAnalyzer = new FFTSongAnalyzer();
            _added = new Subject<Song>();
            
            IndexSongs();
        }

        private void OnApplicationQuit()
        {
            _songReader.Free();
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
                    Song song = Song.Deserialize(songDirectory.FullName);

                    _cachedSongs.Add(song);
                }
                catch
                {
                    // ignore, i guess
                }
            }

            HasContent = _cachedSongs.Count > 0;
        }

        private void AddFromData(string originalSongFile, SongPCM data, SongSpectrum spectrum)
        {
            string id = Guid.NewGuid().ToString();
            string songPath = ContentFileSystem.SongsPath + $"/{id}";
            Directory.CreateDirectory(songPath);

            SongMeta songMeta = new SongMeta
            {
                Name = Path.GetFileNameWithoutExtension(originalSongFile),
                Length = (float)data.Samples / data.Frequency
            };

            Song song = new Song(songPath + "/Song.file")
            {
                Path = songPath,
                Meta = songMeta,
                PCM = data,
                Spectrum = spectrum
            };

            song.Serialize(originalSongFile);
            
            Add(song);
        }
        
        public UniTask Process(string path)
        {
            try
            {
                SongPCM pcm = _songReader.Open(path);
                SongSpectrum spectrum = _songAnalyzer.Analyze(pcm);
                AddFromData(path, pcm, spectrum);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

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
                if (content.PCM == null)
                {
                    return Observable.Start(() =>
                        {
                            SongPCM pcm = _songReader.Open(content.Path);
                            return pcm;
                        })
                        .ObserveOnMainThread()
                        .Select(data =>
                        {
                            content.PCM = data;
                            content.Clip = SongUtils.AssembleClip(content.PCM);
                            content.PCM = null;
                            return content;
                        });
                }
                
                content.Clip = SongUtils.AssembleClip(content.PCM);
                content.PCM = null;
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