using System.Collections.Generic;
using Remaz.Core.Grid;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor.Grid
{
    public class GridDraw : MonoBehaviour
    {
        [SerializeField] private GameObject _lineSegmentPrefab;

        private List<GameObject> _lineInstances;
        
        private void Start()
        {
            _lineInstances = new List<GameObject>();
            
            GenerateGrid(ScreenGrid.Size.Value);

            ScreenGrid.Size
                .Subscribe(GenerateGrid)
                .AddTo(this);
        }

        private void GenerateGrid(Vector2 size)
        {
            _lineInstances.ForEach(Destroy);
            
            Vector2 halfSize = size * 0.5f;
            
            for (int y = 0; y < Mathf.FloorToInt(size.y) + 1; y++)
            {
                CreateLine(-halfSize.x - 0.5f, y - halfSize.y + 0.5f, 0f, size.x + 1f);
            }
            
            for (int x = 0; x < Mathf.FloorToInt(size.x) + 1; x++)
            {
                CreateLine(x - halfSize.x + 0.5f, -halfSize.y - 0.5f, 90f, size.y + 1f);
            }
        }
        
        private void CreateLine(float x, float y, float rotation, float scale)
        {
            Transform lineSegment = Instantiate(_lineSegmentPrefab, transform).transform;
            
            lineSegment.position = new Vector3(x, y, 1f);
            lineSegment.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
            lineSegment.localScale = new Vector3(scale, 1f, 1f);
            
            _lineInstances.Add(lineSegment.gameObject);
        }
    }
}