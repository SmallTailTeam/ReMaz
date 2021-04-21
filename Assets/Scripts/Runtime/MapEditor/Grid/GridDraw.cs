using System.Collections.Generic;
using ReMaz.Grid;
using UniRx;
using UnityEngine;

namespace ReMaz.MapEditor.Grid
{
    public class GridDraw : MonoBehaviour
    {
        [SerializeField] private GridLine _lineSegmentPrefab;

        private List<GridLine> _lineSegments;
        
        private void Start()
        {
            _lineSegments = new List<GridLine>();
            
            GenerateGrid(ScreenGrid.Size.Value);

            ScreenGrid.Size
                .Subscribe(GenerateGrid)
                .AddTo(this);

            Observable.EveryUpdate()
                .Subscribe(_ => PositionGrid())
                .AddTo(this);
        }

        private void PositionGrid()
        {
            foreach (GridLine lineSegment in _lineSegments)
            {
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.x = Mathf.Ceil(cameraPosition.x);
                cameraPosition.z = 0;
                
                lineSegment.transform.position = cameraPosition + lineSegment.Position;
            }
        }
        
        private void GenerateGrid(Vector2 size)
        {
            _lineSegments.ForEach(line => Destroy(line.gameObject));
            _lineSegments.Clear();
            
            Vector2 halfSize = size * 0.5f;
            
            for (int y = 0; y < Mathf.FloorToInt(size.y) + 1; y++)
            {
                CreateLine(-halfSize.x - 0.5f - 1f, y - halfSize.y + 0.5f, 0f, Mathf.Ceil(size.x + 2f));
            }
            
            for (int x = 0; x < Mathf.FloorToInt(size.x) + 4; x++)
            {
                CreateLine(x - halfSize.x + 0.5f - 2f, -halfSize.y - 0.5f, 90f, Mathf.Ceil(size.y + 1f));
            }
        }
        
        private void CreateLine(float x, float y, float rotation, float scale)
        {
            GridLine lineSegment = Instantiate(_lineSegmentPrefab, transform);
            
            lineSegment.transform.position = new Vector3(x, y, 1f);
            lineSegment.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
            lineSegment.transform.localScale = new Vector3(scale, 1f, 1f);

            lineSegment.Position = lineSegment.transform.position;
            
            _lineSegments.Add(lineSegment);
        }
    }
}