using System.Collections;
using System.Collections.Generic;
using System.IO;
using SmallTail.Preload.Attributes;
using UnityEngine;
using UnityEngine.Networking;

namespace ReMaz.Core.Songs
{
    [Preloaded]
    public class SongList : MonoBehaviour
    {
        private static List<CachedSong> _cachedSongs = new List<CachedSong>();

        private void Awake()
        {
            IndexSongs();
        }

        private void IndexSongs()
        {
            if (!Directory.Exists("Songs"))
            {
                return;
            }

            DirectoryInfo directory = new DirectoryInfo("Songs");
            
            foreach (FileInfo songFile in directory.GetFiles())
            {
                CachedSong cachedSong = new CachedSong(songFile.FullName);
                StartCoroutine(LoadAudioClip(cachedSong));
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
        
        public static CachedSong GetRandom()
        {
            CachedSong cachedSong = _cachedSongs[Random.Range(0, _cachedSongs.Count)];
            return cachedSong;
        }
    }
}