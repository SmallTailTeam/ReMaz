using ReMaz.Core.UI;
using ReMaz.Core.UI.Selection;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.Menu.UI
{
    public class ButtonsSelection : MonoBehaviour
    {
        private Image _graphics;
        private ColorScheme _colorScheme;
        
        private ISelectable _selectable;

        private void Awake()
        {
            _graphics = GetComponentsInChildren<Image>()[1];
            _colorScheme = ColorScheme.Get();
            
            _selectable = GetComponent<ISelectable>();
        }

        private void Start()
        {
            _selectable.Selected
                .Subscribe(_ => Selected())
                .AddTo(this);
            
            _selectable.Deselected
                .Subscribe(_ => Deselected())
                .AddTo(this);
        }


        private void Selected()
        {
            _graphics.color = _colorScheme.ActiveColor;
        }

        private void Deselected()
        {
            _graphics.color = _colorScheme.RestColor;
        }
    }
}