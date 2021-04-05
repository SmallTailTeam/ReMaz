using System;
using UnityEngine;

namespace ReMaz.Core.Content.Projects.Tiles
{
    [Serializable]
    public class TileDescription
    {
        public string Id => _id;
        public GameObject Prefab => _prefab;
        public Sprite Icon => _icon;
        public Sprite IconInteracted => _iconInteracted;
        public int Rotation => _rotation;

        [SerializeField] private string _id;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Sprite _iconInteracted;
        [SerializeField, Range(0, 360)] private int _rotation;
    }
}