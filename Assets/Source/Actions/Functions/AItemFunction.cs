using UnityEngine;
using Roguelike.Entities;

namespace Roguelike.Actions
{
    [RequireComponent(typeof(Item))]
    public abstract class AItemFunction : MonoBehaviour
    {
        private Item item;
        public Item Item
        {
            get
            {
                if (item == null)
                    item = GetComponent<Item>();

                return item;
            }
        }

        public abstract IActionResult[] Execute(Entity target);
    }
}
