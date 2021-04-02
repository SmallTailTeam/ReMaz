using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Core.UI.Selection
{
    public class SelectionGroupDebug : MonoBehaviour
    {
        [SerializeField, AutoHook] private SelectableGroup selectableGroup;

        private void Start()
        {
            selectableGroup
                .Selected
                .Subscribe(selectable =>
                {
                    Debug.Log("Selected: " + selectable);
                })
                .AddTo(this);
            
            selectableGroup
                .Deselected
                .Subscribe(selectable =>
                {
                    Debug.Log("Deselected: " + selectable);
                })
                .AddTo(this);
        }
    }
}