using UnityEngine;

namespace ReMaz.UI.ColorPicking.HSV
{
    public class ValueKnob : MonoBehaviour
    {
        [SerializeField] protected HSVColorPicker _colorPicker;
        
        protected RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}