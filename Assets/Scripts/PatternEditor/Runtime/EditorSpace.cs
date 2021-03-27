using Remaz.Game.Map;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorSpace : MonoBehaviour
    {
        [SerializeField] private PlaceZone _placeZone;
        [SerializeField] private GameObject _point;
        
        private void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0) && _placeZone.CanPlace)
                .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition))
                .Subscribe(pos =>
                {
                    GridPosition gridPosition = GridPosition.FromWorld(pos);
                    Debug.Log(gridPosition);
                    Instantiate(_point, transform).transform.position = gridPosition.ToWorld();
                })
                .AddTo(this);
        }
    }
}