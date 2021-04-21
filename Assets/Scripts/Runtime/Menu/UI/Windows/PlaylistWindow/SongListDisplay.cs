using System.Collections.Generic;
using ReMaz.Content;
using ReMaz.Content.Songs;
using UniRx;
using UnityEngine;

namespace ReMaz.Menu.UI.Windows.PlaylistWindow
{
    public class SongListDisplay : MonoBehaviour
    {
        [SerializeField] private SongDisplay _songPrefab; 
        
        private IAsyncContentContainer<Song> _songContainer;

        private List<SongDisplay> _instances;

        private void Awake()
        {
            _instances = new List<SongDisplay>();
            
            _songContainer = FindObjectOfType<Playlist>();
        }

        private void Start()
        {
            foreach (Song song in _songContainer.GetAll())
            {
                Spawn(song);
            }

            _songContainer.Added
                .ObserveOnMainThread()
                .Subscribe(Spawn)
                .AddTo(this);
        }

        private void Spawn(Song song)
        {
            SongDisplay songDisplay = Instantiate(_songPrefab, transform);
            songDisplay.Display(song);
            _instances.Add(songDisplay);
        }
    }
}