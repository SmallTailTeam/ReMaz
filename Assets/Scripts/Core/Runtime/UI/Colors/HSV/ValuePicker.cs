using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReMaz.Core.UI.Colors.HSV
{
    public abstract class ValuePicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] protected HSVColorPicker _colorPicker;
        
        protected RectTransform _rectTransform;

        private bool _dragging;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _dragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => _dragging)
                .Subscribe(_ => Pick())
                .AddTo(this);
        }
        
        protected abstract void Pick();
    }
}