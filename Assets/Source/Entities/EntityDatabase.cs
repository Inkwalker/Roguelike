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

        public Entity GetInstance(Guid instanceID)
        {
            Entity entity;

            instanceDatabase.TryGetValue(instanceID, out entity);

            return entity;
        }

        public void AddInstance(Entity entity)
        {
            instanceDatabase.Add(entity.InstanceID, entity);
        }

        public Entity CreateInstance(string prefabID)
        {
            var prefab = GetPrefab(prefabID);

            var instance = Instantiate(prefab);

            AddInstance(instance);

            return instance;
        }

        public Entity CreateInstance(EntityInstanceData data)
        {
            var prefab = GetPrefab(data.PrefabID);

            var instance = Instantiate(prefab);
            instance.SetData(data);

            AddInstance(instance);

            return instance;
        }

        public void RemoveInstance(Entity entity)
        {
            instanceDatabase.Remove(entity.InstanceID);
        }

        public Entity[] GetAllInstances()
        {
            var result = new List<Entity>(instanceDatabase.Values);
            return result.ToArray();
        }
    }
}
