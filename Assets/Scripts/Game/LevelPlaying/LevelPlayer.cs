using ReMaz.Levels;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelPlayer : MonoBehaviour
    {
        protected Level _level
        {
            get
            {
                if (__level == null && TryGetComponent(out LevelDriver levelContainer))
                {
                    __level = levelContainer.Level;
                }

                return __level;
            }
        }

        private Level __level;
    }
}