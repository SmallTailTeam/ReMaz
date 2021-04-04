using System;
using Un4seen.Bass;
using UnityEngine;

namespace ReMaz.Core
{
    public static class BassManager
    {
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
        
        public static AudioClip Open(string path)
        {
            if (!_initialized)
            {
                Initialize();
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
            
            AudioClip clip = AudioClip.Create("test", samples, 2, (int)freq, false);
            clip.SetData(floatWave, 0);
            
            return clip;
        }
    }
}