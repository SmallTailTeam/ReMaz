using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReMaz.LevelEditing.UI
{
    public class Track : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector] public int TrackIndex;
        
        [SerializeField] public RectTransform _occupiedPrefab;

        private AudioSource _audioSource;
        private LevelEditor _levelEditor;
        private RectTransform _rt;
        private List<float> _fts = new List<float>();
        private bool _hover;

        private void Awake()
        {
            _audioSource = FindObjectOfType<AudioSource>();
            _levelEditor = FindObjectOfType<LevelEditor>();
            _rt = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_hover && Input.GetMouseButtonDown(0))
            {
                float y = GetRelativeMousePosition(out float sizeY).y;

                /*
                int ft = Mathf.FloorToInt(t * LevelEditorUtils.BeatLength / LevelEditorUtils.Scale * 4f / sizeY);

                if (Occupied(ft))
                {
                    return;
                }

                t = (float) ft / LevelEditorUtils.Beats / 4f * LevelEditorUtils.Minutes * 60f;
                
                _levelEditor.Paint(TrackIndex, t);
                _fts.Add(ft);
*/
                float gs = LevelEditorUtils.BeatLength / LevelEditorUtils.SubBeats;
                float pos = Mathf.Floor(y / gs) * gs;

                if (Occupied(pos))
                {
                    return;
                }
                
                _fts.Add(pos);
                
                RectTransform i = Instantiate(_occupiedPrefab, transform);
                i.sizeDelta = new Vector2(i.sizeDelta.x, LevelEditorUtils.BeatLength / LevelEditorUtils.SubBeats);
                i.anchoredPosition = new Vector2(0f, pos);

                pos += (float) LevelEditorUtils.EmptyStart / LevelEditorUtils.MaxScale * LevelEditorUtils.Height;
                float t = pos / sizeY * LevelEditorUtils.Minutes * 60f;
                _levelEditor.Paint(TrackIndex, t);
            }
        }

        private bool Occupied(float ft)
        {
            return _fts.Any(e => Mathf.Approximately(e, ft));
        }

        private Vector2 GetRelativeMousePosition(out float sizeY)
        {
            Vector3[] corners = new Vector3[4];
            _rt.GetWorldCorners(corners);

            Vector3 position = Input.mousePosition;
            
            Rect rect = new Rect(corners[0], corners[2] - corners[0]);

            position.y -= rect.y;

            position.y = Mathf.Clamp(position.y, 0f,  rect.size.y);

            sizeY = rect.size.y;

            return position;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover = false;
        }
    }
}