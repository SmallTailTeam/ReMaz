using Remaz.Game.Map;
using UnityEngine;

namespace ReMaz.PatternEditor.Tools
{
    public abstract class EditorTool : ScriptableObject
    {
        public abstract void Use(Pattern pattern);
    }
}