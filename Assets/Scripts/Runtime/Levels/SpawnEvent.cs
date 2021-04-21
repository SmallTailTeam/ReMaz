using System;
using UnityEngine;

namespace ReMaz.Levels
{
    [Serializable]
    public class SpawnEvent : LevelEvent
    {
        [Range(-1, 1)]
        public int Track;
        public GameObject Prefab;
    }
}