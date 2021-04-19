using System.Collections;
using ReMaz.Game.Inputs;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Game.Player
{
    public class PlayerCircumferenceMovement : MonoBehaviour
    {
        [SerializeField, AutoHook] private PlayerInput _input;
        [SerializeField] private int _sliceCount;
        [SerializeField] private float _interpolationSpeed;

        private Coroutine _interpolation;
        private float _currentSlice;
        
        private void Start()
        {
            _input.Moved
                .Subscribe(Move)
                .AddTo(this);
        }

        private void Move(float direction)
        {
            _currentSlice = Mathf.Repeat(_currentSlice + direction, _sliceCount);

            float angle = _currentSlice / _sliceCount * 360f;

            if (_interpolation != null)
            {
                StopCoroutine(_interpolation);
            }

            _interpolation = StartCoroutine(Interpolate(Quaternion.Euler(0f, 0f, angle)));
        }

        private IEnumerator Interpolate(Quaternion target)
        {
            Quaternion start = transform.rotation;
            
            for (float t = 0f; t <= 1f; t += _interpolationSpeed * Time.deltaTime)
            {
                transform.rotation = Quaternion.Slerp(start, target, t);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}