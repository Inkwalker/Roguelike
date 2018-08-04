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

        public InventoryWindowEvent ItemSelected;
        public InventoryWindowEvent ItemDropped;

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
            gameObject.SetActive(true);

            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            slots.Clear();

            var items = inventory.Items;

            for (int i = 0; i < inventory.Capacity; i++)
            {
                var slot = Instantiate(slotPrefab, grid.transform);

                slot.OnSelected.AddListener(OnItemSelected);

                if (i < items.Length)
                {
                    slot.SetItem(items[i]);
                }

                slots.Add(slot);
            }
        }

        public void Hide()
        {
            if (slots != null)
            {
                foreach (var slot in slots)
                {
                    Destroy(slot.gameObject);
                }

                slots.Clear();
            }

            gameObject.SetActive(false);
        }

        private void OnItemSelected(InventorySlot slot)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                ItemDropped.Invoke(slot.Item);
            }
            else
            {
                ItemSelected.Invoke(slot.Item);
            }
        }

        [System.Serializable]
        public class InventoryWindowEvent : UnityEngine.Events.UnityEvent<Item> { }
    }
}
