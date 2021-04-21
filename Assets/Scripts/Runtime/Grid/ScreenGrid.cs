using SmallTail.Preload.Attributes;
using UniRx;
using UnityEngine;

namespace ReMaz.Grid
{
    public class ScreenGrid : MonoBehaviour
    {
        public static ReactiveProperty<Vector2> Size { get; private set; }

        private void Start()
        {
            Size = new ReactiveProperty<Vector2>();

            Recalculate();

            var widthProperty = Observable.EveryUpdate().Select(_ => Screen.width).ToReadOnlyReactiveProperty();
            var heighthProperty = Observable.EveryUpdate().Select(_ => Screen.height).ToReadOnlyReactiveProperty();
            
            widthProperty.Merge(heighthProperty)
                .Subscribe(_ => Recalculate())
                .AddTo(this);
        }

        private void Recalculate()
        {
            Camera targetCamera = Camera.main;

            if (targetCamera != null)
            {
                Vector3 viewPort = targetCamera.ViewportToWorldPoint(new Vector2(1, 1));

                Size.Value = new Vector2(viewPort.x * 2f - 1f, viewPort.y * 2f - 1f);
            }
        }
    }
}