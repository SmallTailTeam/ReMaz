using System;
using TNRD.Autohook;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReMaz.Core.UI.Colors
{
    public class SaturationValuePicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public IObservable<Vector2> PositionChanged => _positionChanged;
        
        [SerializeField, AutoHook(AutoHookSearchArea.Parent)] private ColorPicker _colorPicker;
        
        private RectTransform _rectTransform;

        private ISubject<Vector2> _positionChanged;
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
            _positionChanged = new Subject<Vector2>();
        }

        private void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => _dragging)
                .Subscribe(_ => Pick())
                .AddTo(this);
        }

        private void Pick()
        {
            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);

            Vector2 position = Input.mousePosition;
            Rect rect = new Rect(corners[0], corners[2] - corners[0]);

            position.x -= rect.x;
            position.y -= rect.y;

            position.x = Mathf.Clamp(position.x, 0f, rect.size.x);
            position.y = Mathf.Clamp(position.y, 0f,  rect.size.y);
            
            _positionChanged?.OnNext(rect.position + position);

            position.x /= rect.size.x;
            position.y /= rect.size.y;

            _colorPicker.S.Value = position.x;
            _colorPicker.V.Value = position.y;
        }
    }
}