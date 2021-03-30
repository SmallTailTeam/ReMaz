using SmallTail.Preload;
using UniRx;
using UnityEngine;

namespace Remaz.Core.Grid
{
    [Preloaded]
    public class ScreenGrid : MonoBehaviour
    {
        public static ReactiveProperty<Vector2> Size { get; private set; }

        private Camera _targetCamera;

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
            
            Size.Value = new Vector2(viewPort.x * 2f - 1f, viewPort.y * 2f - 1f);
        }
    }
}