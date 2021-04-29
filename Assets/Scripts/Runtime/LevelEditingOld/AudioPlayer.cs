using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.LevelEditingOld
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField, AutoHook] private LevelEditor _levelEditor;
        [SerializeField, AutoHook] private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource.clip = _levelEditor.Levelset.Clip;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Toggle();
            }
        }

        private void Toggle()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.Play();
            }
        }
    }
}