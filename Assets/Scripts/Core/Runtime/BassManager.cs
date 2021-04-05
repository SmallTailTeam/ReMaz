using System;
using Cysharp.Threading.Tasks;
using Un4seen.Bass;
using UnityEngine;

namespace ReMaz.Core
{
    public static class BassManager
    {
        private static object _lock = new object();
        private static bool _initialized;

        private static void LogError(string name)
        {
            Debug.LogError($"[BassError] {name}: {Bass.BASS_ErrorGetCode()}");
        }
        
        private static void Initialize()
        {
            _initialized = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            if (!_initialized)
            {
                LogError("Init");
            }
        }

        public static void Free()
        {
            Bass.BASS_Free();
        }

        public static AudioData Open(string path)
        {
            lock (_lock)
            {
                if (!_initialized)
                {
                    Initialize();
                }
            }

            int num = Bass.BASS_StreamCreateFile(path, 0L, 0L, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_UNICODE);

            if (num == 0)
            {
                LogError("StreamCreateFile");
            }

            float freq = 0;
            bool success = Bass.BASS_ChannelGetAttribute(num, BASSAttribute.BASS_ATTRIB_FREQ, ref freq);

            if (!success)
            {
                LogError("Frequency");
            }

            long length = Bass.BASS_ChannelGetLength(num, BASSMode.BASS_POS_BYTES);

            if (length == -1)
            {
                LogError("Length");
            }

            double seconds = Bass.BASS_ChannelBytes2Seconds(num, length);

            if (seconds == -1)
            {
                LogError("Seconds");
            }

            int samples = Mathf.RoundToInt((float) (seconds * freq));

            short[] shortWave = new short[length];
            Bass.BASS_ChannelGetData(num, shortWave, (int) length);

            float[] floatWave = new float[length];

            for (int i = 0; i < length; i++)
            {
                floatWave[i] = shortWave[i] / 32768f;
            }

            Bass.BASS_StreamFree(num);

            AudioData data = new AudioData
            {
                Samples = samples,
                Channels = 2,
                Frequency = (int) freq,
                Wave = floatWave
            };
            
            return data;
        }

        public static AudioClip AssembleClip(AudioData data)
        {
            AudioClip clip = AudioClip.Create("test", data.Samples, data.Channels, data.Frequency, false);
            clip.SetData(data.Wave, 0);
            return clip;
        }
    }

    public class AudioData
    {
        public int Samples;
        public int Channels;
        public int Frequency;
        public float[] Wave;
    }
}