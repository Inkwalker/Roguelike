using UnityEngine;
using System.Collections;
using Roguelike.Entities;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Roguelike.UI
{
    public class InventoryWindow : MonoBehaviour
    {
        [SerializeField]
        private InventorySlot slotPrefab;

        GridLayoutGroup grid;
        List<InventorySlot> slots;

        Inventory currenInventory;

        private void Awake()
        {
            grid = GetComponentInChildren<GridLayoutGroup>();

            //Clear the grid from any mock elements
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                Destroy(grid.transform.GetChild(i).gameObject);
            }

            slots = new List<InventorySlot>();
        }

        public void Show(Inventory inventory)
        {
            ReturnItems(currenInventory);

            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            slots.Clear();

            currenInventory = inventory;

            var items = inventory.Items;

            for (int i = 0; i < inventory.Capacity; i++)
            {
                var slot = Instantiate(slotPrefab, grid.transform);

                slots.Add(slot);

                if (i < items.Length)
                {
                    slot.SetItem(items[i]);
                }
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            ReturnItems(currenInventory);

            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            slots.Clear();

            gameObject.SetActive(false);
        }

        private void ReturnItems(Inventory inventory)
        {
            foreach (var slot in slots)
            {
                var item = slot.Item;

                if (item != null)
                {
                    item.transform.parent = inventory.transform;
                    item.transform.localPosition = Vector3.zero;
                    item.gameObject.SetActive(false);
                }
            }
        }
    }
}
