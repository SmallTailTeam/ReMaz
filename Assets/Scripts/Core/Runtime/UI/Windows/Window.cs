using UnityEngine;
using Uween;

namespace ReMaz.Core.UI.Windows
{
    public class Window : MonoBehaviour
    {
        public void Open()
        {
            TweenSXY.Add(gameObject, 0.2f, 1f, 1f)
                .From(0.8f, 0.8f)
                .EaseInCubic();
            
            TweenA.Add(gameObject, 0.2f, 1f)
                .From(0f)
                .EaseInCubic();
        }

        public void Close()
        {
            TweenSXY.Add(gameObject, 0.2f, 0.8f, 0.8f)
                .From(1f, 1f)
                .EaseOutCubic();
            
            TweenA.Add(gameObject, 0.2f, 0f)
                .From(1f)
                .EaseOutCubic()
                .OnComplete += () => Destroy(gameObject);
        }
    }
}
