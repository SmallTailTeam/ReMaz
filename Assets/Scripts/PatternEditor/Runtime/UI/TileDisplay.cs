﻿using ReMaz.Core.Content.Projects.Tiles;
using ReMaz.Core.UI.Selection;
using TNRD.Autohook;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.PatternEditor.UI
{
    [RequireComponent(typeof(Button))]
    public class TileDisplay : SelectableDisplay<TileDescription>
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Children)] private Image _image;

        public override void Display(TileDescription content)
        {
            Content = content;
            _image.sprite = content.Icon;
            _image.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, content.Rotation));
        }
    }
}