using UnityEngine;

namespace ReMaz.EditorBehaviours.LevelEditing
{
    [RequireComponent(typeof(AudioSource))]
    public class ControllableAudioPlayer : LevelEditorBehaviour
    {
        public AudioSource Source { get; private set; }

        private void Awake()
        {
            Source = GetComponent<AudioSource>();
            
            Source.clip = _levelEditor.Level.LevelSet.Clip;
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
            if (Source.isPlaying)
            {
                Source.Pause();
            }
            else
            {
                Source.Play();
            }
        }
    }
}