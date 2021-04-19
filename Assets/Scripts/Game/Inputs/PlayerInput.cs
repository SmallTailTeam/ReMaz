using System;
using UniRx;
using UnityEngine;

namespace ReMaz.Game.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        public IObservable<float> Moved => _moved;

        [SerializeField] private float _sliceSize = 0.12f;

        private ISubject<float> _moved = new Subject<float>();
        private float _slice;
        private float _startX;
        private int _lastSlice;

        private void Awake()
        {
            _slice = Screen.width * _sliceSize;
        }

        private void Update()
        {
            DoDesktop();
            DoMobile();
        }

        private void DoDesktop()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _moved.OnNext(-1f);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _moved.OnNext(1f);
            }
        }

        private void DoMobile()
        {
            if (Input.touchCount < 1)
            {
                return;
            }

            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                _startX = touch.position.x;
                _lastSlice = 0;
                return;
            }

            float deltaX = touch.position.x - _startX;

            int currentSlice = Mathf.RoundToInt(deltaX / _slice);

            if (currentSlice != _lastSlice)
            {
                _moved.OnNext(Mathf.Sign(currentSlice - _lastSlice));
            }

            _lastSlice = currentSlice;
        }
    }
}
