using EditorBehaviours.LevelEditing.Controls;
using UniRx;
using UnityEngine;

namespace EditorBehaviours.LevelEditing.UI.Beats
{
    public class BeatDisplayManager : LevelEditorBehaviour
    {
        [SerializeField] private BeatDisplay _displayPrefab;

        private ControllableLevelScroll _scroll;

        private RectTransform _rt;
        private BeatDisplay[] _displays;
        private float _pixelsPerUnit;
        private float _height;

        private void Awake()
        {
            _scroll = FindObjectOfType<ControllableLevelScroll>();
            _rt = GetComponent<RectTransform>();
        }

        private void Start()
        {
            ComputePixelsPerUnit();
            InstantiateDisplays();
            PositionDisplays();
            
            _scroll.Scroll
                .Subscribe(_ => PositionDisplays())
                .AddTo(this);
        }
        
        private void InstantiateDisplays()
        {
            _height = _levelEditor.Meta.Size / _levelEditor.Meta.BeatCount * _pixelsPerUnit;
            
            int count = Mathf.CeilToInt(Screen.height / _height) + 1;
            
            _displays = new BeatDisplay[count];

            for (int i = 0; i < count; i++)
            {
                BeatDisplay display = Instantiate(_displayPrefab, transform);
                display.Size(_height);

                _displays[i] = display;
            }
        }
        
        private void ComputePixelsPerUnit()
        {
            Vector3[] corners = new Vector3[4];
            _rt.GetWorldCorners(corners);
            Vector3 size = corners[3] - corners[0];
            _pixelsPerUnit = (Screen.height - (Screen.width - size.x)) / _levelEditor.Meta.ViewSize;
        }
        
        private void PositionDisplays()
        {
            int unit = _levelEditor.Meta.EmptyStart + _scroll.Scroll.Value;
            float scrollPosition = unit * _pixelsPerUnit;
            scrollPosition = -scrollPosition;

            int beat = Mathf.RoundToInt((float) unit / _levelEditor.Meta.Size * _levelEditor.Meta.BeatCount);

            for (int i = 0; i < _displays.Length; i++)
            {
                float displayPosition = scrollPosition + i * _height;
                displayPosition = Mathf.Repeat(displayPosition + _height, Screen.height + _height) - _height;
                
                BeatDisplay display = _displays[i];
                display.Position(displayPosition);
                display.Display(beat + i);
            }
        }
    }
}