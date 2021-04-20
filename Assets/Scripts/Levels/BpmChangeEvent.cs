using System;

namespace ReMaz.Levels
{
    [Serializable]
    public class BpmChangeEvent : LevelEvent
    {
        public int Bpm;
    }
}