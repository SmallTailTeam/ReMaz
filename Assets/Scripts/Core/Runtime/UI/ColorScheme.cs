using UnityEngine;

namespace ReMaz.Core.UI
{
    [CreateAssetMenu(menuName = "ReMaz!/UI/Color scheme", fileName = "ColorScheme")]
    public class ColorScheme : ScriptableObject
    {
        public Color RestColor => _restColor;
        public Color ActiveColor => _activeColor;

        [SerializeField] private Color _restColor;
        [SerializeField] private Color _activeColor;

        public static ColorScheme Get()
        {
            return Resources.Load<ColorScheme>("Settings/ColorScheme");
        }
    }
}