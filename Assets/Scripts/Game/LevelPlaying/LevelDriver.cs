using ReMaz.Levels;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelDriver : MonoBehaviour
    {
        public Level Level => _level;
        
        [SerializeField] private Level _level;

        private float _time;
        
        private void Update()
        {
            float nextTime = _time + Time.deltaTime;
            _level.Drive(_time, nextTime);
            _time = nextTime;
        }
    }
}