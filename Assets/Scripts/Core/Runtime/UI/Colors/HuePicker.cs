using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.Core.UI.Colors
{
    public class HuePicker : MonoBehaviour
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] private ColorPicker _colorPicker;
        
        public void Drag(float hue)
        {
            _colorPicker.H.Value = hue;
        }
    }
}