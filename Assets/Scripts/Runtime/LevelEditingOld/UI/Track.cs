using System.Collections.Generic;
using System.Linq;
using ReMaz.Levels;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReMaz.LevelEditingOld.UI
{
    public class Track : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector] public int TrackIndex;
        
        [SerializeField] public RectTransform _occupiedPrefab;

        private AudioSource _audioSource;
        private LevelEditor _levelEditor;
        private RectTransform _rt;
        private List<float> _fts = new List<float>();
        private List<GameObject> _is = new List<GameObject>();
        private bool _hover;

        private void Awake()
        {
            _audioSource = FindObjectOfType<AudioSource>();
            _levelEditor = FindObjectOfType<LevelEditor>();
            _rt = GetComponent<RectTransform>();
        }

        private void Start()
        {
            foreach (SpawnEvent se in _levelEditor.Level.Events.OfType<SpawnEvent>().Where(e => e.Track == TrackIndex))
            {
                float pos = se.Position;
                pos *= LevelEditorUtils.Scale;
                //pos -= (float) LevelEditorUtils.EmptyStart / LevelEditorUtils.MaxScale * LevelEditorUtils.Height;
                
                _fts.Add(pos);
                
                RectTransform i = Instantiate(_occupiedPrefab, transform);
                i.sizeDelta = new Vector2(i.sizeDelta.x, LevelEditorUtils.BeatLength / LevelEditorUtils.SubBeats);
                i.anchoredPosition = new Vector2(0f, pos);
                _is.Add(i.gameObject);
            }
        }

        private void Update()
        {
            if (_hover && Input.GetMouseButtonDown(0))
            {
                float y = GetRelativeMousePosition().y;
                SpawnAt(y);
            }

            if (_hover && Input.GetMouseButtonDown(1))
            {
                float y = GetRelativeMousePosition().y;

                float gs = LevelEditorUtils.BeatLength / LevelEditorUtils.SubBeats;
                float pos = Mathf.Floor(y / gs) * gs;

                if (!Occupied(pos))
                {
                    return;
                }

                for (int j = 0; j < _fts.Count; j++)
                {
                    if (Mathf.Approximately(_fts[j], pos))
                    {
                        _fts.RemoveAt(j);
                        Destroy(_is[j]);
                        _is.RemoveAt(j);
                    }
                }

                //pos += (float) LevelEditorUtils.EmptyStart / LevelEditorUtils.MaxScale * LevelEditorUtils.Height;
                pos /= LevelEditorUtils.Scale;
                _levelEditor.Remove(TrackIndex, pos);
            }
        }

        private void SpawnAt(float y, bool ghost = false)
        {
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
            _is.Add(i.gameObject);
            
            if (!ghost)
            {
                //pos += (float) LevelEditorUtils.EmptyStart / LevelEditorUtils.MaxScale * LevelEditorUtils.Height;
                pos /= LevelEditorUtils.Scale;
                _levelEditor.Paint(TrackIndex, pos);
            }
        }
        
        private bool Occupied(float ft)
        {
            return _fts.Any(e => Mathf.Approximately(e, ft));
        }

        private Vector2 GetRelativeMousePosition()
        {
            Vector3[] corners = new Vector3[4];
            _rt.GetWorldCorners(corners);

            Vector3 position = Input.mousePosition;
            
            Rect rect = new Rect(corners[0], corners[2] - corners[0]);

            position.y -= rect.y;

            position.y = Mathf.Clamp(position.y, 0f,  rect.size.y);

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