using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelPlayer : MonoBehaviour
    {
        protected LevelDriver _levelDriver
        {
            get
            {
                if (__levelDriver == null && TryGetComponent(out LevelDriver levelDriver))
                {
                    __levelDriver = levelDriver;
                }

                return __levelDriver;
            }
        }

        private LevelDriver __levelDriver;
    }
}