using UnityEngine;

namespace ReMaz.Core.UI.ColorPicking.HSV
{
    public class HuePicker : ValuePicker
    {
        protected override void Pick()
        {
            Vector2 position = GetRelativeMousePosition();

            position.x = -1f * (2f * position.x - 1f);
            position.y = -1f * (2f * position.y - 1f);
            
            _colorPicker.H.Value = (Mathf.Atan2(position.y, position.x) + Mathf.PI) / (2f * Mathf.PI);
        }
    }
}