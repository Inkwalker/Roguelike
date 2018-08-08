using UnityEngine;
using System.Collections.Generic;
using Roguelike.Actions;
using Roguelike.Dungeon;
using Roguelike.LoadSave;

namespace Roguelike.Entities
{
    public class Inventory : AEntityComponent
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

        public bool Contains(Item item)
        {
            return items.Contains(item);
        }

        public void Remove(Item item)
        {
            items.Remove(item);
        }

        public IActionResult[] Use(Item item, Entity target)
        {
            var results = item.Use(target);

            foreach (var result in results)
            {
                if (result is UseItemActionResult)
                {
                    var useAction = result as UseItemActionResult;

                    if(useAction.Consumed)
                    {
                        Remove(item);
                        Destroy(item.gameObject);
                    }
                }
            }

            return results;
        }

        public IActionResult[] Drop(Item item)
        {
            Remove(item);

            item.transform.parent = null;
            item.Entity.Position = Entity.Position;

            map.AddEntity(item.Entity);

            item.gameObject.SetActive(true);

            return new IActionResult[]
            {
                new DropItemActionResult() { Item = item, Message = string.Format("You dropped the {0}", item.Entity.DisplayName) }
            };
        }

        public override AEntityComponentData GetData()
        {
            return null; //TODO
        }
    }
}
