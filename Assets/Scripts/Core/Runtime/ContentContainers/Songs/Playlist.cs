using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmallTail.Preload.Attributes;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace ReMaz.Core.ContentContainers.Songs
{
    [Preloaded]
    public class Playlist : MonoBehaviour, IAsyncContentContainer<Song>
    {
        private static List<Song> _cachedSongs = new List<Song>();

        private void Awake()
        {
            IndexSongs();
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
                    string songFile = songDirectory.FullName + @"\Song.mp3";
                    string songMetaFile = songDirectory.FullName + @"\Meta.file";

                    string songMetaJson = File.ReadAllText(songMetaFile);
                    SongMeta songMeta = JsonConvert.DeserializeObject<SongMeta>(songMetaJson);
                    
                    Song song = new Song(songFile)
                    {
                        Title = songDirectory.Name,
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

        private IEnumerator LoadAudioClip(Song song)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(song.Path, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    song.Clip = clip;
                }
            }
        }
        
        public IEnumerator GetRandomAsync(Action<Song> got)
        {
            Song song = GetRandom();
            
            if (song.Clip == null)
            {
                yield return StartCoroutine(LoadAudioClip(song));
            }

            got?.Invoke(song);
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