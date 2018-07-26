using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Roguelike.Entities;

namespace Roguelike.UI
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        Transform itemParent;

        public Item Item { get; private set; }

        public SlotEvent OnSelected;

        public void SetItem(Item item)
        {
            item.transform.parent = itemParent;
            item.transform.localPosition = Vector3.zero;

            item.gameObject.SetActive(true);

            Item = item;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Item != null) OnSelected.Invoke(this);
        }

        [System.Serializable]
        public class SlotEvent : UnityEvent<InventorySlot> { }
    }
}
