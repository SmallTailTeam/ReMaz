using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.LevelEditing.UI
{
    public class Waveform : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LevelEditor _levelEditor;
        [SerializeField] private LevelScale levelScale;
        [SerializeField] private Image _image;

        private AudioClip _clip;
        private float[] _waveForm;
        private float[] _samples;

        private void Awake()
        {
            _clip = _levelEditor.Levelset.Clip;
            
            float minutes = _clip.samples / _clip.frequency / 60f;
            int beats = Mathf.CeilToInt(minutes * _levelEditor.Levelset.Bpm);
            
            float beatLength = _levelEditor.Levelset.Bpm / 60f * LevelEditorUtils.Scale;
            float height = beats * beatLength;
            
            int resolution = _clip.frequency / LevelEditorUtils.Resolution;
            int wtf = _clip.samples * _clip.channels;
            LevelEditorUtils.MaxScale = wtf/resolution;
            
            LevelEditorUtils.Minutes = minutes;
            LevelEditorUtils.Beats = beats;
            LevelEditorUtils.BeatLength = beatLength;
            LevelEditorUtils.Height = height;
        }

        private void Start()
        {
            Compute();
            Render(0, _waveForm.Length);
        }

        private void Compute()
        {
            int resolution = _clip.frequency / LevelEditorUtils.Resolution;
 
            _samples = new float[_clip.samples*_clip.channels];
            _clip.GetData(_samples,0);
 
            _waveForm = new float[(_samples.Length/resolution)];
 
            for (int i = 0; i < _waveForm.Length; i++)
            {
                _waveForm[i] = 0;
 
                for(int ii = 0; ii<resolution; ii++)
                {
                    _waveForm[i] += Mathf.Abs(_samples[(i * resolution) + ii]);
                }          
 
                _waveForm[i] /= resolution;
            }

            int count = 0;
            
            for (int i = 0; i < _waveForm.Length; i++)
            {
                float wave = _waveForm[i];

                if (wave < 0.001f)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            LevelEditorUtils.EmptyStart = count;

            count = 0;
            
            for (int i = _waveForm.Length-1; i >= 0; i--)
            {
                float wave = _waveForm[i];

                if (wave < 0.001f)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            LevelEditorUtils.EmptyEnd = count;

            float max = _waveForm.Max();
            float min = _waveForm.Min();
            
            for (int i = 0; i < _waveForm.Length; i++)
            {
                _waveForm[i] = Mathf.InverseLerp(min, max, _waveForm[i]);
            }
        }
        
        private void Render(int skip, int take)
        {
            float[] waveForm = _waveForm.Skip(skip).Take(take).ToArray();
            
            _image.material.SetInt("_Length", waveForm.Length);

            ComputeBuffer buffer = new ComputeBuffer(waveForm.Length, sizeof(float));
            buffer.SetData(waveForm);
            
            _image.material.SetBuffer("_Waveform", buffer);
        }
    }
}