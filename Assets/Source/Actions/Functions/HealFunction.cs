using UnityEngine;
using Roguelike.Entities;

namespace Roguelike.Actions
{
    public class HealFunction : AItemFunction
    {
        [SerializeField] private int healAmount;

        public override IActionResult[] Execute(Entity target)
        {
            var fighter = target.GetComponent<Fighter>();

            if (fighter != null)
            {
                return fighter.Heal(healAmount);
            }

            return new IActionResult[0];
        }
    }
}
