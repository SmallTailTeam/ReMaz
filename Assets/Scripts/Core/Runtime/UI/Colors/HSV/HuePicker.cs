using UnityEngine;

namespace ReMaz.Core.UI.Colors.HSV
{
    public class HuePicker : ValuePicker
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

            position.x = -1f * (2f * position.x - 1f);
            position.y = -1f * (2f * position.y - 1f);
            
            _colorPicker.H.Value = (Mathf.Atan2(position.y, position.x) + Mathf.PI) / (2f * Mathf.PI);
        }
    }
}