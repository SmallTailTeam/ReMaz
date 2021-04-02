using ReMaz.Core.UI;
using UnityEngine;

namespace ReMaz.Game
{
    public class AudioSpeed : MonoBehaviour
    {
        private AudioSource _audioSource;
        private float[] _samples = new float[512];
        private float[] _bands = new float[8];

        private void Awake()
        {
            SongPlayer songPlayer = FindObjectOfType<SongPlayer>();
            _audioSource = songPlayer.AudioSource;
        }

        public float GetAudioMultiplier()
        {
            try
            {
                _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
            }
            catch
            {
                //ignore
            }

            int count = 0;

            for (int i = 0; i < 8; i++)
            {
                float average = 0f;
                int samplecount = (int) Mathf.Pow(2, i) * 2;

                for (int j = 0; j < samplecount; j++)
                {
                    average += _samples[count] * (count + 1);
                    count++;
                }

                average /= count;

                _bands[i] = average;
            }

            float speed = 0f;

            for (int i = 0; i < 8; i++)
            {
                speed += _bands[i];
            }

            return speed;
        }
    }
}