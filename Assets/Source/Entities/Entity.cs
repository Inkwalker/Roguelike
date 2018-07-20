﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using Roguelike.Dungeon;

namespace Roguelike.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] string displayName = "Thing";
        [SerializeField] bool blocks;

        private Vector2 worldPosition;
        private Vector2Int position;

        public string DisplayName { get { return displayName; } }
        public bool Blocks { get { return blocks; } }

        public Vector2Int Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position != value)
                {
                    Moving.Invoke(this);

                    transform.localPosition = new Vector3(
                        value.x * GameMap.TileSize + GameMap.TileSize / 2f,
                        0,
                        value.y * GameMap.TileSize + GameMap.TileSize / 2f);

                    position = value;

                    Moved.Invoke(this);
                }
            }
        }

        public Vector2 WorldPosition
        {
            get
            {
                return new Vector2(transform.position.x, transform.position.z);
            }
            set
            {
                transform.position = new Vector3(value.x, 0, value.y);

                Vector2Int pos = new Vector2Int(
                    Mathf.RoundToInt((transform.localPosition.x - GameMap.TileSize / 2f) / GameMap.TileSize),
                    Mathf.RoundToInt((transform.localPosition.z - GameMap.TileSize / 2f) / GameMap.TileSize));

                if (position != value)
                {
                    Moving.Invoke(this);

                    position = pos;

                    Moved.Invoke(this);
                }
            }
        }

        public EntityEvent Moving;
        public EntityEvent Moved;

        [Serializable]
        public class EntityEvent : UnityEvent<Entity> { }
    }
}