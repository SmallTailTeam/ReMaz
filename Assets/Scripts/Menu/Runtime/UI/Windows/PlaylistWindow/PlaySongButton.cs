using ReMaz.Core.Content.Songs;
using ReMaz.Core.UI;
using UnityEngine;

namespace ReMaz.Menu.UI.Windows.PlaylistWindow
{
    public class PlaySongButton : MonoBehaviour
    {
        private SongPlayer _songPlayer;
        private IDisplay<Song> _songDisplay;

        private void Awake()
        {
            _songPlayer = FindObjectOfType<SongPlayer>();
            _songDisplay = GetComponent<IDisplay<Song>>();
        }

        public void Play()
        {
            _songPlayer.Play(_songDisplay.Content);
        }
    }
}