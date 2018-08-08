using Roguelike.LoadSave;
using UnityEngine;

namespace Roguelike.Entities
{
    public abstract class AEntityComponent : MonoBehaviour
    {
        private Entity entity;

        public Entity Entity
        {
            get
            {
                if (entity == null)
                    entity = GetComponent<Entity>();

                return entity;
            }
        }

        public abstract AEntityComponentData GetData();
    }
}
