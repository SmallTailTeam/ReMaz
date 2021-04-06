using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.Core.UI;
using TNRD.Autohook;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.PatternEditor.UI
{
    [RequireComponent(typeof(Button))]
    public class TileDisplay : MonoBehaviour, IDisplay<TileDescription>
    {
        public TileDescription Data { get; private set; }

        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private Image _image;

        public void Display(TileDescription data)
        {
            Data = data;
            _image.sprite = data.Icon;
            _image.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, data.Rotation));
        }
    }
}