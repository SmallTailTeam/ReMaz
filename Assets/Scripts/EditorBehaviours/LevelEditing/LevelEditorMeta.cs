﻿using System.Linq;
using ReMaz.Levels;
using UnityEngine;

namespace ReMaz.EditorBehaviours.LevelEditing
{
    public class LevelEditorMeta
    {
        public int Resolution;
        public int Scale;
        public int SubBeatCount;
        public float BeatCount;
        public float BeatLength;
        public float Minutes;
        public int MaxScale;
        public int EmptyStart;
        public int EmptyEnd;
        public float Height;
        public int SamplesTotal;
        public float[] Waveform;

        public void Compute(LevelSet levelSet, int resolution, int subBeatCount, int scale)
        {
            AudioClip clip = levelSet.Clip;

            Resolution = clip.frequency / resolution;
            
            Resolution = resolution;
            SubBeatCount = subBeatCount;
            Scale = scale;
            
            SamplesTotal = clip.samples * clip.channels;
            MaxScale = SamplesTotal / Resolution;

            Minutes = (float)clip.samples / clip.frequency / 60f;
            BeatCount = Minutes * levelSet.Bpm;
            
            BeatLength = levelSet.Bpm / 60f * Scale;
            Height = BeatCount * BeatLength;
            
            ComputeWaveform(clip);
        }

        private void ComputeWaveform(AudioClip clip)
        {
            float[] samples = new float[SamplesTotal];
            clip.GetData(samples,0);
 
            Waveform = new float[MaxScale];
 
            for (int i = 0; i < Waveform.Length; i++)
            {
                Waveform[i] = 0;
 
                for(int ii = 0; ii<Resolution; ii++)
                {
                    Waveform[i] += Mathf.Abs(samples[(i * Resolution) + ii]);
                }          
 
                Waveform[i] /= Resolution;
            }

            int count = 0;
            
            for (int i = 0; i < Waveform.Length; i++)
            {
                float wave1 = Waveform[i];
                float wave2 = Waveform[i + 1];

                if (wave2 - wave1 > 0.002f)
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