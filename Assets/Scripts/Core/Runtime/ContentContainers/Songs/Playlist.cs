using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SmallTail.Preload.Attributes;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace ReMaz.Core.ContentContainers.Songs
{
    [Preloaded]
    public class Playlist : MonoBehaviour, IAsyncContentContainer<CachedSong>
    {
        private static List<CachedSong> _cachedSongs = new List<CachedSong>();

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
            
            foreach (FileInfo songFile in directory.GetFiles())
            {
                CachedSong cachedSong = new CachedSong(songFile.FullName);
                _cachedSongs.Add(cachedSong);
            }
        }

        private IEnumerator LoadAudioClip(CachedSong cachedSong)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(cachedSong.Path, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    cachedSong.Clip = clip;
                }
            }
        }
        
        public IEnumerator GetRandomAsync(Action<CachedSong> got)
        {
            CachedSong cachedSong = _cachedSongs[Random.Range(0, _cachedSongs.Count)];
            
            if (cachedSong.Clip == null)
            {
                yield return StartCoroutine(LoadAudioClip(cachedSong));
            }

            got?.Invoke(cachedSong);
        }
    }
}