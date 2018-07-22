using UnityEngine;
using Roguelike.Entities;

namespace Roguelike.UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField]
        Transform itemParent;

        public Item Item { get; private set; }

        public void SetItem(Item item)
        {
            item.transform.parent = itemParent;
            item.transform.localPosition = Vector3.zero;

            item.gameObject.SetActive(true);

            Item = item;
        }
    }
}
