using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.Core.Songs
{
    public class SongPlayer : MonoBehaviour
    {
        public AudioSource AudioSource => _audioSource;
        
        [SerializeField, AutoHook] private AudioSource _audioSource;

        private void Start()
        {
            PlayRandom();
        }

        private void PlayRandom()
        {
            CachedSong cachedSong = SongList.GetRandom();
            _audioSource.clip = cachedSong.Clip;
            _audioSource.Play();
        }
    }
}