using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.Core.UI.Selection
{
    public class SelectionColor : MonoBehaviour
    {
        private Graphic _graphic;
        private ColorScheme _colorScheme;
        
        private ISelectable _selectable;

        private void Awake()
        {
            TryGetComponent(out Image selfImage);
            
            foreach (Graphic graphic in GetComponentsInChildren<Graphic>())
            {
                if (graphic != selfImage)
                {
                    _graphic = graphic;
                    break;
                }
            }
            
            _colorScheme = ColorScheme.Get();
            
            _selectable = GetComponent<ISelectable>();
            
            _selectable.Selected
                .Subscribe(_ => Selected())
                .AddTo(this);
            
            _selectable.Deselected
                .Subscribe(_ => Deselected())
                .AddTo(this);
        }

        private void Selected()
        {
            _graphic.color = _colorScheme.ActiveColor;
        }

        private void Deselected()
        {
            _graphic.color = _colorScheme.RestColor;
        }
    }
}