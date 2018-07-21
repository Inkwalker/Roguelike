using UnityEngine;

namespace Roguelike.Entities
{
    public class EntityComponent : MonoBehaviour
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
    }
}
