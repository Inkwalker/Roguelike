using UnityEngine;
using System.Collections.Generic;
using Roguelike.Entities;

namespace Roguelike.Dungeon
{
    public class EntityMap
    {
        private List<Entity>[,] entities;

        public EntityMap(int width, int height)
        {
            entities = new List<Entity>[width, height];
        }

        public void Add(Entity entity)
        {
            var pos = entity.Position;

            if (entities[pos.x, pos.y] == null)
            {
                entities[pos.x, pos.y] = new List<Entity>();
            }

            entities[pos.x, pos.y].Add(entity);

            entity.Moving.AddListener(OnEntityMoving);
            entity.Moved.AddListener(OnEntityMoved);
        }

        public void Remove(Entity entity)
        {
            var pos = entity.Position;

            entity.Moving.RemoveListener(OnEntityMoving);
            entity.Moved.RemoveListener(OnEntityMoved);

            if (entities[pos.x, pos.y] == null) return;

            entities[pos.x, pos.y].Remove(entity);
        }

        public Entity GetTop(Vector2Int position)
        {
            if (entities[position.x, position.y] == null) return null;

            int index = entities[position.x, position.y].Count - 1;

            return entities[position.x, position.y][index];
        }

        public Entity[] GetAll(Vector2Int position)
        {
            if (entities[position.x, position.y] == null) return new Entity[0];

            return entities[position.x, position.y].ToArray();
        }

        public Entity GetBlocking(Vector2Int position)
        {
            if (entities[position.x, position.y] == null) return null;

            foreach (var entity in entities[position.x, position.y])
            {
                if (entity.Blocks) return entity;
            }

            return null;
        }

        private void OnEntityMoving(Entity entity)
        {
            var pos = entity.Position;
            if (entities[pos.x, pos.y] == null) return;

            entities[pos.x, pos.y].Remove(entity);
        }

        private void OnEntityMoved(Entity entity)
        {
            var pos = entity.Position;

            if (entities[pos.x, pos.y] == null)
            {
                entities[pos.x, pos.y] = new List<Entity>();
            }

            entities[pos.x, pos.y].Add(entity);
        }
    }
}
