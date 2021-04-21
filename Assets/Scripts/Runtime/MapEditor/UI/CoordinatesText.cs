using ReMaz.MapEditor.Inputs;
using TMPro;
using TNRD.Autohook;
using UniRx;
using UnityEngine;

namespace ReMaz.MapEditor.UI
{
    public class CoordinatesText : MonoBehaviour
    {
        [SerializeField, AutoHook] private TMP_Text _text;

        private EditorInputs _inputs;

        private void Awake()
        {
            _inputs = FindObjectOfType<EditorInputs>();
        }

        private void Start()
        {
            _inputs.PointerGridPositionStream.ToReadOnlyReactiveProperty()
                .Subscribe(pos => _text.text = pos.ToString())
                .AddTo(this);
        }
    }
}