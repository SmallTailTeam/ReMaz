using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelAudioPlayer : LevelPlayer
    {
        [SerializeField, AutoHook] private AudioSource _audioSource;

        private void Start()
        {
            Invoke(nameof(Replay), 5);
        }

        private void Replay()
        {
            _audioSource.clip = _levelDriver.LevelSet.Clip;
            _audioSource.Play();
        }
    }
}