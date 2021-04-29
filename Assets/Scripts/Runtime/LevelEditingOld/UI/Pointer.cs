using TNRD.Autohook;
using UnityEngine;

namespace ReMaz.LevelEditingOld.UI
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private LevelScale levelScale;
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
            int resolution = _audioSource.clip.frequency / LevelEditorUtils.Resolution;
            
            float y = (float) _audioSource.timeSamples/resolution / LevelEditorUtils.Scale * _containerSize.y;
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