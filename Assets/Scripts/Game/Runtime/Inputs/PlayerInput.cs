using UniRx;
using UnityEngine;

namespace Remaz.Game.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<Vector3> MousePosition { get; private set; }

        private void Awake()
        {
            MousePosition = Observable.EveryUpdate().Select(_ => Input.mousePosition).ToReadOnlyReactiveProperty();
        }
    }
}
