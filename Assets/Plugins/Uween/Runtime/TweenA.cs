using UnityEngine;
using UnityEngine.UI;

namespace Uween
{
    public class TweenA : TweenVec1
    {
        public static TweenA Add(GameObject g, float duration)
        {
            return Add<TweenA>(g, duration);
        }

        public static TweenA Add(GameObject g, float duration, float to)
        {
            return Add<TweenA>(g, duration, to);
        }

        private Graphic G;
        private CanvasGroup GR;

        protected Graphic GetGraphic()
        {
            if (G == null)
            {
                G = GetComponent<Graphic>();
            }

            return G;
        }
        
        protected CanvasGroup GetGroup()
        {
            if (GR == null)
            {
                GR = GetComponent<CanvasGroup>();
            }

            return GR;
        }

        protected override float Value
        {
            get => GetGraphic()?.color.a ?? GetGroup().alpha;
            set
            {
                var g = GetGraphic();

                if (g != null)
                {
                    var c = g.color;
                    c.a = value;
                    g.color = c;
                    return;
                }

                var gr = GetGroup();

                if (gr != null)
                {
                    gr.alpha = value;
                }
            }
        }
    }
}