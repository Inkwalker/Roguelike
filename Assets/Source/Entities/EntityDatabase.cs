using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Entities
{
    public class EntityDatabase : MonoBehaviour
    {
        [SerializeField] Entity[] entities;

        private Dictionary<string, Entity> prefabDatabase;

        private void Awake()
        {
            prefabDatabase = new Dictionary<string, Entity>();

            foreach (var entity in entities)
            {
                prefabDatabase.Add(entity.PrefabID, entity);
            }
        }

        public Entity GetPrefab(string prefabID)
        {
            return prefabDatabase[prefabID];
        }
    }
}
