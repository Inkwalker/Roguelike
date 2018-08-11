using UnityEngine;
using System.Collections.Generic;
using System;
using Roguelike.LoadSave;

namespace Roguelike.Entities
{
    public class EntityDatabase : MonoBehaviour
    {
        [SerializeField] Entity[] entities;

        private Dictionary<string, Entity> prefabDatabase = new Dictionary<string, Entity>();
        private Dictionary<Guid, Entity> instanceDatabase = new Dictionary<Guid, Entity>();

        private void Awake()
        {
            foreach (var entity in entities)
            {
                prefabDatabase.Add(entity.PrefabID, entity);
            }
        }

        public Entity GetPrefab(string prefabID)
        {
            return prefabDatabase[prefabID];
        }

        public Entity GetInstance(Guid entityID)
        {
            Entity entity;

            instanceDatabase.TryGetValue(entityID, out entity);

            return entity;
        }

        public void AddInstance(Entity entity)
        {
            instanceDatabase.Add(entity.EntityID, entity);
        }

        public Entity CreateInstance(string prefabID)
        {
            var prefab = GetPrefab(prefabID);

            var instance = Instantiate(prefab);

            AddInstance(instance);

            return instance;
        }

        public Entity CreateInstance(string prefabID, Guid entityID)
        {
            var prefab = GetPrefab(prefabID);

            var instance = Instantiate(prefab);
            instance.EntityID = entityID;

            AddInstance(instance);

            return instance;
        }

        public bool RemoveInstance(Entity entity)
        {
            return instanceDatabase.Remove(entity.EntityID);
        }

        public Entity[] GetAllInstances()
        {
            var result = new List<Entity>(instanceDatabase.Values);
            return result.ToArray();
        }
    }
}
