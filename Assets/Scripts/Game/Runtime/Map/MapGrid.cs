using SmallTail.Preload;
using UniRx;
using UnityEngine;

namespace Remaz.Game.Map
{
    [Preloaded("MapGrid")]
    public class MapGrid : MonoBehaviour
    {
        public static ReactiveProperty<Vector2> Size { get; private set; }

        private Camera _targetCamera;

        public static GridPosition Snap(Vector3 position)
        {
            position.x += Size.Value.x * 0.5f;
            
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            
            return new GridPosition(x, y);
        }

        public static Vector3 UnSnap(Vector3 position)
        {
            position.x -= Size.Value.x * 0.5f;

            float halfWidth = Size.Value.x * 0.5f;
            float halfHeight = Size.Value.y * 0.5f;
            
            position.x = Mathf.Clamp(position.x, -halfWidth, halfWidth);
            position.y = Mathf.Clamp(position.y, -halfHeight, halfHeight);
            
            return position;
        }
        
        private void Start()
        {
            Size = new ReactiveProperty<Vector2>();
            _targetCamera = Camera.main;
            
            Recalculate();

            var widthProperty = Observable.EveryUpdate().Select(_ => Screen.width).ToReadOnlyReactiveProperty();
            var heighthProperty = Observable.EveryUpdate().Select(_ => Screen.height).ToReadOnlyReactiveProperty();
            
            widthProperty.Merge(heighthProperty)
                .Subscribe(_ => Recalculate())
                .AddTo(this);
        }

        private void Recalculate()
        {
            Vector3 viewPort = _targetCamera.ViewportToWorldPoint(new Vector2(1, 1));
            
            Size.Value = new Vector2(viewPort.x * 2f, viewPort.y * 2f);
        }
    }
}