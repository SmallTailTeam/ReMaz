using UniRx;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelMovement : LevelPlayer
    {
        private ReadOnlyReactiveProperty<int> _bpm;
        
        private void Start()
        {
            _bpm = _level.BpmChanges
                .Select(e => e.Bpm)
                .ToReadOnlyReactiveProperty();
        }
    }
}