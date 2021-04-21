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
        [SerializeField] private float _interpolationSpeed;

        private Coroutine _interpolation;
        
        private void Start()
        {
            _input.TrackChange
                .Subscribe(OnMoved)
                .AddTo(this);
        }

        private void OnMoved(int track)
        {
            if (_interpolation != null)
            {
                StopCoroutine(_interpolation);
            }
            
            Quaternion to = Quaternion.Euler(0f, 0f, track * 60f);
            _interpolation = StartCoroutine(QuaternionInterpolate(transform, to, _interpolationSpeed));
        }

        private static IEnumerator QuaternionInterpolate(Transform target, Quaternion to, float speed)
        {
            Quaternion from = target.rotation;

            for (float t = 0; t < 1f; t += Time.deltaTime * speed)
            {
                target.rotation = Quaternion.Slerp(from, to, t);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}