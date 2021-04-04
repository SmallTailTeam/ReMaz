using ReMaz.Core.ContentContainers.Songs;
using SFB;
using UnityEngine;

namespace ReMaz.Menu.UI.Windows.PlaylistWindow
{
    public class AddSongFileBrowser : MonoBehaviour
    {
        private Playlist _playlist;

        private void Awake()
        {
            _playlist = FindObjectOfType<Playlist>();
        }

        public void Open()
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Select songs", "", "", true);

            foreach (string path in paths)
            {
                _playlist.Process(path);
            }
        }
    }
}