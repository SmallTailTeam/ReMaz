using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReMaz.UI.ColorPicking.HSV
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

        protected Vector2 GetRelativeMousePosition()
        {
            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);

            Vector2 position = Input.mousePosition;
            Rect rect = new Rect(corners[0], corners[2] - corners[0]);

            position.x -= rect.x;
            position.y -= rect.y;

            position.x = Mathf.Clamp(position.x, 0f, rect.size.x);
            position.y = Mathf.Clamp(position.y, 0f,  rect.size.y);

            position.x /= rect.size.x;
            position.y /= rect.size.y;

            return position;
        }
        
        protected abstract void Pick();
    }
}