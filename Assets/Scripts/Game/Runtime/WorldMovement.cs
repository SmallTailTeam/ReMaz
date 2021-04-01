using UniRx;
using UnityEngine;

namespace ReMaz.Game
{
    public class WorldMovement : MonoBehaviour
    {
        private LevelMovement _levelMovement;

        private void Awake()
        {
            _levelMovement = FindObjectOfType<LevelMovement>();
        }

        private void Start()
        {
            _levelMovement.Moved
                .Subscribe(Moved)
                .AddTo(this);
        }

        private void Moved(float movement)
        {
            transform.position -= new Vector3(movement, 0f, 0f);
        }
    }
}