using System.Collections.Generic;
using ReMaz.Levels;
using UnityEngine;

namespace ReMaz.LevelEditing
{
    public class LevelEditor : MonoBehaviour
    {
        public LevelSet Levelset => _levelSet;
        public Level Level => _levelSet.Levels[_levelIndex];
        
        [SerializeField] private LevelSet _levelSet;
        [SerializeField] private int _levelIndex;
        [SerializeField] private List<SpawnEvent> _spawnEvents;

        private void OnValidate()
        {
            if (Level == null)
            {
                return;
            }
            Level.Events.Clear();
            Level.Events.AddRange(_spawnEvents);
        }
    }
}