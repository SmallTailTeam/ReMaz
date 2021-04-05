using Cysharp.Threading.Tasks;
using ReMaz.Core.ContentContainers;
using ReMaz.Core.ContentContainers.Songs;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI
{
    public class SongPlayer : MonoBehaviour
    {
        public AudioSource AudioSource => _audioSource;
        
        [SerializeField, AutoHook] private AudioSource _audioSource;

        private IAsyncContentContainer<Song> _songContainer;

        private void Awake()
        {
            _songContainer = FindObjectOfType<Playlist>();
        }

        private void Start()
        {
            PlayRandom();
        }

        public void PlayRandom()
        {
            Song song = _songContainer.GetRandom();
            Play(song);
        }

        public void Play(Song song)
        {
            if (song != null)
            {
                _songContainer.GetAsync(song)
                    .Subscribe(song =>
                    {
                        _audioSource.clip = song.Clip;
                        _audioSource.Play();
                    })
                    .AddTo(this);
            }
        }
    }
}