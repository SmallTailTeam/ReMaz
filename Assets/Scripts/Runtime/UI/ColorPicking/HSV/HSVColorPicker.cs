using System;
using UniRx;
using UnityEngine;

namespace ReMaz.UI.ColorPicking.HSV
{
    public class HSVColorPicker : ColorPicker
    {
        public override IObservable<Unit> Updated { get; protected set; }
        public override ReadOnlyReactiveProperty<Color> PickedColor { get; protected set; }
        public ReactiveProperty<float> H { get; private set; }
        public ReactiveProperty<float> S { get; private set; }
        public ReactiveProperty<float> V { get; private set; }

        private void Awake()
        {
            H = new ReactiveProperty<float>();
            S = new ReactiveProperty<float>();
            V = new ReactiveProperty<float>();

            PickedColor = Observable.Merge(H, S, V).Select(_ => Color.HSVToRGB(H.Value, S.Value, V.Value)).ToReadOnlyReactiveProperty();
            Updated = Observable.Merge(H, S, V).Select(_ => Unit.Default);
        }
        
        public override void Pick(Color color)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            
            FromHSV(h, s, v);
        }

        public override void AsHSV(out float h, out float s, out float v)
        {
            h = H.Value;
            s = S.Value;
            v = V.Value;
        }

        public override void FromHSV(float h, float s, float v)
        {
            H.Value = h;
            S.Value = s;
            V.Value = v;
        }
    }
}