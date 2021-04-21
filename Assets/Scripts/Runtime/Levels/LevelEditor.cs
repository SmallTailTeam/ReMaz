using System.Collections.Generic;
using ReMaz.Levels;
using UnityEngine;

namespace ReMaz.Editor.Levels
{
    public class LevelEditor : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private List<SpawnEvent> _spawnEvents;

        private void OnValidate()
        {
            if (_level == null)
            {
                return;
            }
            _level.Events.Clear();
            _level.Events.AddRange(_spawnEvents);
        }
    }
}