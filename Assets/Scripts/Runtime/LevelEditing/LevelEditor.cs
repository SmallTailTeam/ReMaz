using System.Collections.Generic;
using System.Linq;
using ReMaz.Levels;
using UnityEditor;
using UnityEngine;

namespace ReMaz.LevelEditing
{
    public class LevelEditor : MonoBehaviour
    {
        public LevelSet Levelset => _levelSet;
        public Level Level => _levelSet.Levels[_levelIndex];
        
        [SerializeField] private LevelSet _levelSet;
        [SerializeField] private int _levelIndex;
        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private List<SpawnEvent> _spawnEvents;

        private void OnValidate()
        {
            if (Level == null)
            {
                return;
            }
            //Level.Events.Clear();
            //Level.Events.AddRange(_spawnEvents);
        }

        public void Paint(int trackIndex, float t)
        {
            List<LevelEvent> list = Level.Events.ToList();
            list.Add(new SpawnEvent
            {
                Time = t,
                Track = trackIndex,
                Prefab = _objectPrefab
            });
            Level.Events = list.ToArray();
            
            EditorUtility.SetDirty(_levelSet);
            EditorUtility.SetDirty(Level);
        }
    }
}