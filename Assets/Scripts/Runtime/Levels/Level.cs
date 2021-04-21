using System.Collections.Generic;
using UnityEngine;

namespace ReMaz.Levels
{
    [CreateAssetMenu(menuName = "ReMaz!/Levels/Level", fileName = "Level")]
    public class Level : ScriptableObject
    {
        public string Name;
        public float DistanePerBeat;
        public List<LevelEvent> Events;
    }
}