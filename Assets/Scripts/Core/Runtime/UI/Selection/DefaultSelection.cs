using UnityEngine;

namespace ReMaz.Core.UI.Selection
{
    public class DefaultSelection : MonoBehaviour
    {
        private ISelectable _selectable;

        private void Awake()
        {
            _selectable = GetComponent<ISelectable>();
        }

        private void Start()
        {
            _selectable.Select();
        }
    }
}