using UnityEngine;
using System.Collections.Generic;
using Roguelike.Actions;
using Roguelike.Dungeon;

namespace Roguelike.Entities
{
    public class Inventory : EntityComponent
    {
        [SerializeField] int capacity = 16;

        private GameMap map;

        private List<Item> items;
        public Item[] Items { get { return items.ToArray(); } }
        public int Capacity { get { return capacity; } }

        private void Awake()
        {
            items = new List<Item>(capacity);
            map = FindObjectOfType<GameMap>();
        }

        public AddItemActionResult Add(Item item)
        {
            if(items.Count < capacity)
            {
                items.Add(item);

                map.RemoveEntity(item.Entity);

                item.gameObject.SetActive(false);
                item.transform.parent = transform;

                return new AddItemActionResult()
                {
                    Item = item,
                    Message = string.Format("You pick up the {0}!", item.Entity.DisplayName)
                };
            }
            else
            {
                return new AddItemActionResult()
                {
                    Message = string.Format("You cannot carry any more, your inventory is full")
                };
            }
        }
    }
}
