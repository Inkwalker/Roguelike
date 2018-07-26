using UnityEngine;
using Roguelike.Entities;
using System.Collections.Generic;

namespace Roguelike.Actions
{
    public class HealFunction : AItemFunction
    {
        [SerializeField] private int healAmount;

        public override IActionResult[] Execute(Entity target)
        {
            var results = new List<IActionResult>();

            var fighter = target.GetComponent<Fighter>();

            if (fighter != null)
            {
                if (fighter.HP == fighter.MaxHP)
                {
                    results.Add(new UseItemActionResult(Item, false, "You are already at full health"));
                }
                else
                {
                    fighter.Heal(healAmount);
                    results.Add(new UseItemActionResult(Item, true, "Your wounds start to feel better!"));
                }
            }

            return results.ToArray();
        }
    }
}
