﻿using UnityEngine;

namespace ReMaz.LevelEditing.UI
{
    public class Playback : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LevelScroll _levelScroll;
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
            
            y *= Mathf.RoundToInt(((float)_levelScroll.Scale.Value / _levelScroll.MaxScale * 0.8f + 0.2f) * _scrollSpeed);

            int newTime = _audioSource.timeSamples - y;

            newTime = Mathf.Clamp(newTime, 0, _audioSource.clip.samples);

            _audioSource.timeSamples = newTime;
        }
    }
}