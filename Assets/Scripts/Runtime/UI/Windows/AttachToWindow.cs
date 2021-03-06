using System;
using ReMaz.UI.Windows;
using ReMaz.UI.Selection;
using UniRx;
using UnityEngine;

namespace ReMaz.UI.Windows
{
    public class AttachToWindow : MonoBehaviour
    {
        public IObservable<Window> Attached => _attached;
        
        [SerializeField] private Window _window;

        private WindowManager _windowManager;
        private ISelectable _selectable;

        private ISubject<Window> _attached = new Subject<Window>();
        private Window _windowInstance;
        
        private void Awake()
        {
            _windowManager = FindObjectOfType<WindowManager>();
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
            if (_windowInstance == null)
            {
                _windowInstance = _windowManager.Open(_window);

                _attached.OnNext(_windowInstance);
                
                _windowInstance.Closed
                    .Subscribe(_ =>
                    {
                        _windowInstance = null;
                        _selectable.TargetGroup.Deselect();
                    })
                    .AddTo(_windowInstance);
            }
        }

        private void Deselected()
        {
            if (_windowInstance != null)
            {
                _windowInstance.Close();
            }
        }
    }
}