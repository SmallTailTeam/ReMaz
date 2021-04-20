using System.Collections.Generic;
using UnityEngine;

namespace ReMaz.Levels
{
    [CreateAssetMenu(menuName = "ReMaz!/Levels/Level set", fileName = "LevelSet")]
    public class LevelSet : ScriptableObject
    {
        public string Name;
        public AudioClip Clip;
        public List<Level> Levels;
    }
}