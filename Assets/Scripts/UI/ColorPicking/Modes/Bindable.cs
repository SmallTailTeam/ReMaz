using System;
using UniRx;
using UnityEngine;

namespace ReMaz.UI.ColorPicking.Modes
{
    public abstract class Bindable<T> : MonoBehaviour
    {
        public IObservable<T> ValueChanged => _valueChanged;
        public T Value => _value;

        protected ISubject<T> _valueChanged = new Subject<T>();
        protected T _value;

        private IDisposable _subscription;

        public void Bind(IObservable<T> binding)
        {
            _subscription?.Dispose();

            _subscription = binding
                .Subscribe(Change)
                .AddTo(this);
        }

        protected virtual void InternalChange(T value)
        {
            if (_value.Equals(value))
            {
                return;
            }

            _value = value;
            _valueChanged.OnNext(value);
        }
        
        protected abstract void Change(T value);
    }
}