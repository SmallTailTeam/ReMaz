using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.LevelEditing.UI
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private LevelScroll _levelScroll;
        [SerializeField] private RectTransform _container;
        [SerializeField, AutoHook] private RectTransform _self;
        [SerializeField] private AudioSource _audioSource;

        private Vector2 _containerSize;
        
        private void Start()
        {
            _containerSize = GetContainerSize();
        }

        private void Update()
        {
            int resolution = _audioSource.clip.frequency / LevelEditorSettings.ResolutionX2;
            
            float y = (float) _audioSource.timeSamples/resolution / _levelScroll.Scale.Value * _containerSize.y;
            _self.anchoredPosition = new Vector2(0f, y);
        }
        
        private Vector2 GetContainerSize()
        {
            Vector3[] corners = new Vector3[4];
            _container.GetWorldCorners(corners);

            Rect rect = new Rect(corners[0], corners[2] - corners[0]);

            return rect.size;
        }
    }
}