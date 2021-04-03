using ReMaz.Core.ContentContainers;
using ReMaz.Core.ContentContainers.Songs;
using UnityEngine;

namespace ReMaz.Menu.UI.Windows.PlaylistWindow
{
    public class SongListDisplay : MonoBehaviour
    {
        [SerializeField] private SongDisplay _songPrefab; 
        
        private IAsyncContentContainer<Song> _songContainer;

        private void Awake()
        {
            _songContainer = FindObjectOfType<Playlist>();
        }

        private void Start()
        {
            foreach (Song song in _songContainer.GetAll())
            {
                SongDisplay songDisplay = Instantiate(_songPrefab, transform);
                songDisplay.Display(song);
            }
        }
    }
}