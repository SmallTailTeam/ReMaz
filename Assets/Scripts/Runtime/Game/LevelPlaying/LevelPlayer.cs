using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.Game.LevelPlaying
{
    public class LevelPlayer : MonoBehaviour
    {
        [SerializeField, AutoHook] protected LevelDriver _levelDriver;
    }
}