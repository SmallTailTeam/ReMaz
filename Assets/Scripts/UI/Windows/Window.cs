using System;
using UniRx;
using UnityEngine;
using Uween;

namespace ReMaz.UI.Windows
{
    public class Window : MonoBehaviour
    {
        public IObservable<Unit> Closed => _closed;

        private ISubject<Unit> _closed;

        private void Awake()
        {
            _closed = new Subject<Unit>();
        }

        public void Open()
        {
            TweenSXY.Add(gameObject, 0.2f, 1f, 1f)
                .From(0.8f, 0.8f)
                .EaseInCubic();
            
            TweenA.Add(gameObject, 0.2f, 1f)
                .From(0f)
                .EaseInCubic();
        }

        public void Close()
        {
            _closed.OnNext(Unit.Default);
            
            TweenSXY.Add(gameObject, 0.2f, 0.8f, 0.8f)
                .From(1f, 1f)
                .EaseOutCubic();
            
            TweenA.Add(gameObject, 0.2f, 0f)
                .From(1f)
                .EaseOutCubic()
                .OnComplete += () => Destroy(gameObject);
        }
    }
}
