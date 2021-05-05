using System.Linq;
using ReMaz.Levels;
using UnityEngine;

namespace EditorBehaviours.LevelEditing
{
    public class LevelEditorMeta
    {
        public int WaveformResolution;
        public int ViewSize;
        public int Size;
        public float BeatCount;
        public float BeatLength;
        public float Minutes;
        public int EmptyStart;
        public int EmptyEnd;
        public float Height;
        public int SamplesTotal;
        public float[] Waveform;

        public void Compute(LevelSet levelSet, int waveformResolution, int viewSize)
        {
            AudioClip clip = levelSet.Clip;

            WaveformResolution = clip.frequency / waveformResolution;
            
            WaveformResolution = waveformResolution;
            ViewSize = viewSize;
            
            SamplesTotal = clip.samples * clip.channels;
            Size = SamplesTotal / WaveformResolution;

            Minutes = (float)clip.samples / clip.frequency / 60f;
            BeatCount = Minutes * levelSet.Bpm;
            
            BeatLength = levelSet.Bpm / 60f * ViewSize;
            Height = BeatCount * BeatLength;
            
            ComputeWaveform(clip);
        }

        private void ComputeWaveform(AudioClip clip)
        {
            float[] samples = new float[SamplesTotal];
            clip.GetData(samples,0);
 
            Waveform = new float[Size];
 
            for (int i = 0; i < Waveform.Length; i++)
            {
                Waveform[i] = 0;
 
                for(int ii = 0; ii<WaveformResolution; ii++)
                {
                    Waveform[i] += Mathf.Abs(samples[(i * WaveformResolution) + ii]);
                }          
 
                Waveform[i] /= WaveformResolution;
            }

            int count = 0;
            
            for (int i = 0; i < Waveform.Length; i++)
            {
                float wave1 = Waveform[i];
                float wave2 = Waveform[i + 1];

                if (wave2 - wave1 > 0.005f)
                {
                    count = i;
                    break;
                }
            }

            EmptyStart = count;

            count = 0;
            
            for (int i = Waveform.Length-1; i >= 0; i--)
            {
                float wave = Waveform[i];

                if (wave < 0.01f)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            EmptyEnd = count;

            float max = Waveform.Max();
            float min = Waveform.Min();
            
            for (int i = 0; i < Waveform.Length; i++)
            {
                Waveform[i] = Mathf.InverseLerp(min, max, Waveform[i]);
            }
        }
    }
}