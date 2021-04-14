using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.ColorPicking
{
    public abstract class ColorPicker : MonoBehaviour
    {
        public abstract ReadOnlyReactiveProperty<Color> PickedColor { get; protected set; }

        public abstract void Pick(Color color);
    }
}