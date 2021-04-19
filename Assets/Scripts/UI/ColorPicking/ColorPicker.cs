using System;
using UniRx;
using UnityEngine;

namespace ReMaz.UI.ColorPicking
{
    public abstract class ColorPicker : MonoBehaviour
    {
        public abstract IObservable<Unit> Updated { get; protected set; }
        public abstract ReadOnlyReactiveProperty<Color> PickedColor { get; protected set; }

        public abstract void Pick(Color color);

        public virtual void AsHSV(out float h, out float s, out float v)
        {
            Color.RGBToHSV(PickedColor.Value, out h, out s, out v);
        }

        public virtual void FromHSV(float h, float s, float v)
        {
            Pick(Color.HSVToRGB(h, s, v));
        }
    }
}