using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.LevelEditing.UI
{
    public class Waveform : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LevelEditor _levelEditor;
        [SerializeField] private LevelScroll _levelScroll;
        [SerializeField] private Image _image;

        private AudioClip _clip;
        private float[] _waveForm;
        private float[] _samples;
 
        private void Start()
        {
            _clip = _levelEditor.Levelset.Clip;
            
            int resolution = _clip.frequency / LevelEditorSettings.Resolution;
 
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

                if (wave < 0.01f)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            LevelEditorSettings.Empty = count;

            float max = _waveForm.Max();
            float min = _waveForm.Min();
            
            for (int i = 0; i < _waveForm.Length; i++)
            {
                _waveForm[i] = Mathf.InverseLerp(min, max, _waveForm[i]);
            }

            float minutes = _clip.samples / _clip.frequency / 60f;
            int beats = Mathf.RoundToInt(minutes * _levelEditor.Levelset.Bpm);
            
            float beatLength = _levelEditor.Levelset.Bpm / 60f * _levelScroll.MaxScale / Screen.height;
            float height = beats * beatLength;

            RectTransform rt = _image.GetComponent<RectTransform>();
            
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
            Render(0, _waveForm.Length);
            
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    float y = (float) _audioSource.timeSamples/resolution / _levelScroll.Scale.Value * height / 5f;

                    rt.anchoredPosition = new Vector2(0f, -y);
                })
                .AddTo(this);
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