using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.ColorPicking.HSV
{
    public class HSVColorPicker : ColorPicker
    {
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
        }
        
        public override void Pick(Color color)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            
            H.Value = h;
            S.Value = s;
            V.Value = v;
        }
    }
}