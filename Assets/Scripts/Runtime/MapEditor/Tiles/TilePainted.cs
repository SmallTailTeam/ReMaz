﻿using ReMaz.Grid;
using UnityEngine;

namespace ReMaz.MapEditor.Tiles
{
    public class TilePainted
    {
        public string Id;
        public GameObject Instance;
        public SpriteRenderer Graphics;
        public GridPosition Position;
        public Color Color;

        public TilePainted(string id, GameObject instance, GridPosition position)
        {
            Id = id;
            Instance = instance;
            Position = position;
        }
    }
}