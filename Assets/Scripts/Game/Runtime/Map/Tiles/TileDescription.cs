using System;
using UnityEngine;

namespace Remaz.Game.Map.Tiles
{
    [Serializable]
    public class TileDescription
    {
        public string Id => _id;
        public GameObject Prefab => _prefab;
        public Sprite Icon => _icon;

        [SerializeField] private string _id;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _icon;
    }
}