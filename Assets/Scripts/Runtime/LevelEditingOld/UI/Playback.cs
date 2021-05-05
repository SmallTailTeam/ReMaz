using UnityEngine;

namespace LevelEditingOld.UI
{
    public class Playback : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LevelScale levelScale;
        [SerializeField] private int _scrollSpeed;

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                return;
            }
            
            int y = -Mathf.RoundToInt(Input.mouseScrollDelta.y);

            if (y == 0)
            {
                return;
            }
            
            y *= Mathf.FloorToInt(_audioSource.clip.frequency / LevelEditorUtils.SubBeats);

            int newTime = _audioSource.timeSamples - y;

            newTime = Mathf.Clamp(newTime, 0, _audioSource.clip.samples);

            _audioSource.timeSamples = newTime;
        }
    }
}