using ReMaz.Core.ContentContainers;
using ReMaz.Core.ContentContainers.Songs;
using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.Core.UI
{
    public class SongPlayer : MonoBehaviour
    {
        public AudioSource AudioSource => _audioSource;
        
        [SerializeField, AutoHook] private AudioSource _audioSource;

        private IAsyncContentContainer<CachedSong> _playlist;

        private void Awake()
        {
            _playlist = FindObjectOfType<Playlist>();
        }

        private void Start()
        {
            PlayRandom();
        }

        private void PlayRandom()
        {
            StartCoroutine(_playlist.GetRandomAsync(cachedSong =>
            {
                _audioSource.clip = cachedSong.Clip;
                _audioSource.Play();
            }));
        }
    }
}