using UnityEngine;

namespace Roguelike.LoadSave
{
    [System.Serializable]
    public class FighterComponentData : AEntityComponentData
    {
        [SerializeField] int hp;

        public int HP { get { return hp; } set { hp = value; } }
    }
}
