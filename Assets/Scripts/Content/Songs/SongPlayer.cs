using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Content.Songs
{
    public class SongPlayer : MonoBehaviour
    {
        public AudioSource AudioSource => _audioSource;
        public Song Playing { get; private set; }
        
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
            if (song == null)
            {
                return;
            }
        
            CancelInvoke(nameof(PlayRandom));
            
            _songContainer.GetAsync(song)
                .Subscribe(filledSong =>
                {
                    Playing = filledSong;
                    
                    _audioSource.clip = filledSong.Clip;
                    _audioSource.Play();

                    Invoke(nameof(PlayRandom), filledSong.Meta.Length);
                })
                .AddTo(this);
        }
    }
}