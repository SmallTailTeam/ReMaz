using System.Collections.Generic;
using System.Linq;
using ReMaz.Levels;
using UnityEditor;
using UnityEngine;

namespace LevelEditingOld
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
                Position = t,
                Track = trackIndex,
                Prefab = _objectPrefab
            });
            Level.Events = list.ToArray();
            
            EditorUtility.SetDirty(_levelSet);
            EditorUtility.SetDirty(Level);
        }

        public void Remove(int trackIndex, float t)
        {
            List<LevelEvent> list = Level.Events.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                LevelEvent e = list[i];

                if (e is SpawnEvent se)
                {
                    if (se.Track == trackIndex && se.Position == t)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
            }
            Level.Events = list.ToArray();
            
            EditorUtility.SetDirty(_levelSet);
            EditorUtility.SetDirty(Level);
        }
    }
}