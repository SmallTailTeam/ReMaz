using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.Core.UI.Selection
{
    public class SelectionColor : MonoBehaviour
    {
        private Image _graphics;
        private ColorScheme _colorScheme;
        
        private ISelectable _selectable;

        private void Awake()
        {
            TryGetComponent(out Image selfImage);
            
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                if (image != selfImage)
                {
                    _graphics = image;
                    break;
                }
            }
            
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