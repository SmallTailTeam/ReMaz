using UnityEngine;

namespace ReMaz.Core.UI.Colors.HSV
{
    public class SaturationValuePicker : ValuePicker
    {
        protected override void Pick()
        {
            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);

            Vector2 position = Input.mousePosition;
            Rect rect = new Rect(corners[0], corners[2] - corners[0]);

            position.x -= rect.x;
            position.y -= rect.y;

            position.x = Mathf.Clamp(position.x, 0f, rect.size.x);
            position.y = Mathf.Clamp(position.y, 0f,  rect.size.y);

            position.x /= rect.size.x;
            position.y /= rect.size.y;

            _colorPicker.S.Value = position.x;
            _colorPicker.V.Value = position.y;
        }
    }
}