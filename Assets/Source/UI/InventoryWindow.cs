using UnityEngine;
using Roguelike.Entities;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Roguelike.UI
{
    public class InventoryWindow : MonoBehaviour
    {
        [SerializeField] GameObject content;
        [SerializeField] InventorySlot slotPrefab;
        [SerializeField] GridLayoutGroup grid;

        List<InventorySlot> slots;

        public bool Visible { get; private set; }

        public InventoryWindowEvent ItemSelected;
        public InventoryWindowEvent ItemDropped;

        private void Awake()
        {
            //Clear the grid from any mock elements
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                Destroy(grid.transform.GetChild(i).gameObject);
            }

            slots = new List<InventorySlot>();

            Visible = content.activeSelf;
        }

        public void Show(Inventory inventory)
        {
            content.SetActive(true);

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

            Visible = content.activeSelf;
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

            content.SetActive(false);

            Visible = content.activeSelf;
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
