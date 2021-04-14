using UnityEngine;

namespace ReMaz.Core.UI.ColorPicking.HSV
{
    public class SaturationValuePicker : ValuePicker
    {
        protected override void Pick()
        {
            Vector2 position = GetRelativeMousePosition();

            _colorPicker.S.Value = position.x;
            _colorPicker.V.Value = position.y;
        }
    }
}