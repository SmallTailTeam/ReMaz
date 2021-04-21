using UniRx;
using UnityEngine;

namespace ReMaz.UI.ColorPicking.Modes.HSV
{
    public class HSVMode : MonoBehaviour
    {
        [SerializeField] private ColorPicker _colorPicker;
        [SerializeField] private Bindable<float> _hField;
        [SerializeField] private Bindable<float> _sField;
        [SerializeField] private Bindable<float> _vField;

        private ISubject<float> _hChanged = new Subject<float>();
        private ISubject<float> _sChanged = new Subject<float>();
        private ISubject<float> _vChanged = new Subject<float>();
        
        private void Start()
        {
            _colorPicker.Updated
                .Subscribe(_ => ColorUpdated())
                .AddTo(this);
            
            _hField.Bind(_hChanged);
            _sField.Bind(_sChanged);
            _vField.Bind(_vChanged);
            
            ColorUpdated();

            Observable.Merge(_hField.ValueChanged, _sField.ValueChanged, _vField.ValueChanged)
                .Subscribe(_ => _colorPicker.FromHSV(_hField.Value, _sField.Value, _vField.Value))
                .AddTo(this);
        }

        private void ColorUpdated()
        {
            _colorPicker.AsHSV(out float h, out float s, out float v);

            _hChanged.OnNext(h);
            _sChanged.OnNext(s);
            _vChanged.OnNext(v);
        }
    }
}