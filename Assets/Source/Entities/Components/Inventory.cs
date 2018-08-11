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

        private Item[] items;
        public Item[] Items { get { return items; } }
        public int Capacity { get { return capacity; } }
        public int ItemsCount { get; private set; }

        private void Awake()
        {
            items = new Item[capacity];
            map = FindObjectOfType<GameMap>();
        }

        public AddItemActionResult Add(Item item)
        {
            if(ItemsCount < capacity)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] == null)
                    {
                        Items[i] = item;
                        ItemsCount++;

                        break;
                    }
                }

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
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == item) return true;
            }

            return false;
        }

        public bool Remove(Item item)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == item)
                {
                    Items[i] = null;
                    return true;
                }
            }

            return false;
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
            var data = new InventoryComponentData();

            data.SetItems(items);

            return data;
        }

        public override void SetData(EntityInstanceData data)
        {
            var entityDatabase = FindObjectOfType<EntityDatabase>();

            var invData = data.GetComponentData<InventoryComponentData>();

            if (invData != null)
            {
                for (int i = 0; i < Capacity; i++)
                {
                    string id = invData.GetItem(i);

                    if (id != string.Empty)
                    {
                        var item = entityDatabase.GetInstance(new System.Guid(id));

                        Add(item.GetComponent<Item>());
                    }
                }
            }
        }
    }
}
