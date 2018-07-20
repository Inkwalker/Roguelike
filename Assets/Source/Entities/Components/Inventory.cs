using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Entities
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int capacity = 16;

        public List<Item> Items { get; private set; }

        private void Awake()
        {
            Items = new List<Item>(capacity);
        }
    }
}
