using System;
using ReMaz.Content.Songs;
using ReMaz.UI;
using TMPro;
using UnityEngine;

namespace ReMaz.Menu.UI.Windows.PlaylistWindow
{
    public class SongDisplay : MonoBehaviour, IDisplay<Song>
    {
        public Song Content { get; private set; }

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _duration;

        public void Display(Song content)
        {
            _title.text = content.Meta.Name;
            _duration.text = TimeSpan.FromSeconds(content.Meta.Length).ToString(@"mm\:ss");

            Content = content;
        }
    }
}