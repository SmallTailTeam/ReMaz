using System;
using UnityEngine;

namespace ReMaz.Levels
{
    [Serializable]
    public struct LevelSelection
    {
        public LevelSet LevelSet => _levelSet;
        public Level Level => _levelSet.Levels[_levelIndex];
        
        [SerializeField] private LevelSet _levelSet;
        [SerializeField] private int _levelIndex;

        public LevelSelection(LevelSet levelSet, int levelIndex)
        {
            _levelSet = levelSet;
            _levelIndex = levelIndex;
        }
    }
}