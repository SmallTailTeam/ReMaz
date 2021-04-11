using ReMaz.PatternEditor.Inputs;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _fastSpeed = 15f;
        
        private EditorInputs _inputs;

        private void Awake()
        {
            _inputs = FindObjectOfType<EditorInputs>();
        }

        private void Start()
        {
            _inputs.CameraMovementStream
                .Subscribe(Move)
                .AddTo(this);
        }

        private void Move(float movement)
        {
            float speed = _inputs.MoveFaster.Value ? _fastSpeed : _speed;
            
            Vector3 position = transform.position;
            position.x += movement * speed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, 0f, 100000000f);
            
            transform.position = position;
        }
    }
}