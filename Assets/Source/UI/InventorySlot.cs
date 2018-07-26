using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Roguelike.Entities;

namespace Roguelike.UI
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] float rotationSpeed = 10f;
        [SerializeField] Transform itemParent;

        public Item Item { get; private set; }

        public SlotEvent OnSelected;

        private void Update()
        {
            itemParent.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }

        public void SetItem(Item item)
        {
            var icon = Instantiate(item.Icon, itemParent);

            icon.transform.localPosition = Vector3.zero;

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
