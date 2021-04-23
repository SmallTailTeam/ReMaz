using UnityEngine;

namespace ReMaz.Levels
{
    [CreateAssetMenu(menuName = "ReMaz!/Levels/Level", fileName = "Level")]
    public class Level : ScriptableObject
    {
        [SerializeField] public string Name;
        [SerializeField] public int TrackCount;
        [SerializeField] public float DistanePerBeat;
        [SerializeReference]
        [SerializeField] public LevelEvent[] Events;
    }
}