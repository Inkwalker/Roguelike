using Roguelike.Entities;
using System.Collections.Generic;

namespace Roguelike.LoadSave
{
    [System.Serializable]
    public class InventoryComponentData : AEntityComponentData
    {
        private string[] content;

        public void SetItems(Item[] items)
        {
            content = new string[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    content[i] = items[i].Entity.EntityID.ToString();
                }
                else
                {
                    content[i] = string.Empty;
                }
            }
        }

        public string GetItem(int itemSlot)
        {
            return content[itemSlot];
        }
    }
}
