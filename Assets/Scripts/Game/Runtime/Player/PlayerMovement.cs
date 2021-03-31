using ReMaz.Core.Grid;
using ReMaz.Game.Inputs;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.Game.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, AutoHook] private PlayerInput _playerInput;

        [Header("Settings")]
        [SerializeField, Range(0f, 1f)] private float _t;

        private float _targetPosition;

        private void Start()
        {
            _playerInput.MousePosition.Subscribe(MouseMoved).AddTo(this);
            Observable.EveryLateUpdate().Subscribe(Move).AddTo(this);
            
            ScreenGrid.Size.Subscribe(size =>
            {
                transform.position = new Vector3(-size.x * 0.5f + 2f, transform.position.y, transform.position.z);
            }).AddTo(this);
        }

        private void MouseMoved(Vector3 position)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            float height = ScreenGrid.Size.Value.y * 0.5f;
            _targetPosition = Mathf.Clamp(worldPosition.y, -height, height);
        }

        private void Move(long _)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, _targetPosition, transform.position.z), _t);
        }
    }
}