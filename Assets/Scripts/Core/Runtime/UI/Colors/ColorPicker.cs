using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.Colors
{
    public class ColorPicker : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<Color> FinalColor { get; private set; }
        public ReactiveProperty<float> H { get; private set; }
        public ReactiveProperty<float> S { get; private set; }
        public ReactiveProperty<float> V { get; private set; }

        private void Awake()
        {
            H = new ReactiveProperty<float>();
            S = new ReactiveProperty<float>();
            V = new ReactiveProperty<float>();

            FinalColor = Observable.Merge(H, S, V).Select(_ => Color.HSVToRGB(H.Value, S.Value, V.Value)).ToReadOnlyReactiveProperty();
        }
    }
}