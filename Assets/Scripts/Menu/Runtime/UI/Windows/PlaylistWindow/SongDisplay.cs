using System;
using ReMaz.Core.ContentContainers.Songs;
using ReMaz.Core.UI;
using TMPro;
using UnityEngine;

namespace ReMaz.Menu.UI.Windows.PlaylistWindow
{
    public class SongDisplay : MonoBehaviour, IDisplay<Song>
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _duration;
        
        public void Display(Song data)
        {
            _title.text = data.Meta.Name;
            _duration.text = TimeSpan.FromSeconds(data.Meta.Length).ToString(@"mm\:ss");
        }
    }
}