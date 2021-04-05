using ReMaz.Core.Content.Songs;
using UnityEngine;

namespace ReMaz.Game
{
    public class AudioSpeed : MonoBehaviour
    {
        private SongPlayer _songPlayer;

        private void Awake()
        {
            _songPlayer = FindObjectOfType<SongPlayer>();
        }

        public float GetAudioMultiplier()
        {
            int index = GetIndexFromTime(_songPlayer.AudioSource.time) / 1024 / _songPlayer.Playing.Spectrum.StoreEvery;

            float average = _songPlayer.Playing.Spectrum.Averages[index];

            return average;
        }
        
        private int GetIndexFromTime(float time = 0f)
        {
            float lengthPerSample = _songPlayer.Playing.Clip.length / _songPlayer.Playing.Clip.samples;
            return Mathf.FloorToInt (time / lengthPerSample);
        }
    }
}