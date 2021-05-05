using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace LevelEditingOld
{
    public class LevelScale : MonoBehaviour
    {
        [SerializeField, AutoHook] private LevelEditor _levelEditor;
        [SerializeField] private int _scaleSpeed = 100;

        private void Update()
        {
            //DoScale();
        }

        private void DoScale()
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                return;
            }
            
            int y = -Mathf.RoundToInt(Input.mouseScrollDelta.y);

            if (y == 0)
            {
                return;
            }
            
            y *= _scaleSpeed;

            int newValue = LevelEditorUtils.Scale + y;

            newValue = Mathf.Clamp(newValue, 500, LevelEditorUtils.MaxScale);
            
            LevelEditorUtils.Scale = newValue;
        }
    }
}