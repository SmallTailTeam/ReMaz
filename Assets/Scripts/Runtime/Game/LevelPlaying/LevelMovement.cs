using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelMovement : LevelPlayer
    {
        public float Speed { get; private set; }

        private void Awake()
        {
            Speed = _levelDriver.Level.DistanePerBeat * _levelDriver.LevelSet.Bpm / 60f;
        }

        private void Update()
        {
            transform.position -= new Vector3(0f, 0f, Speed * Time.deltaTime);
        }
    }
}