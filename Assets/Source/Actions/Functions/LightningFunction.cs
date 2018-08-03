using UnityEngine;
using Roguelike.Entities;
using Roguelike.Dungeon;
using System.Collections.Generic;

namespace Roguelike.Actions
{
    public class LightningFunction : AItemFunction
    {
        [SerializeField] float range = 5;
        [SerializeField] int damage = 1;

        public override IActionResult[] Execute(Entity target)
        {
            var results = new List<IActionResult>();

            var closestTarget = FindTarget();

            if (closestTarget != null)
            {
                results.Add(
                    new UseItemActionResult(Item, true,
                    string.Format("A lighting bolt strikes the {0} with a loud thunder! The damage is {1}", closestTarget.Entity.DisplayName, damage))
                );

                results.AddRange(closestTarget.TakeDamage(damage));
            }
            else
            {
                results.Add(
                    new UseItemActionResult(Item, false,
                    "No enemy is close enough to strike.")
                );
            }

            return results.ToArray();
        }

        private Fighter FindTarget()
        {
            var map = FindObjectOfType<GameMap>();
            var ownerInv = GetComponentInParent<Inventory>();
            var owner = ownerInv != null ? ownerInv.Entity : null;

            var position = owner != null ? owner.Position : Item.Entity.Position;

            Fighter target = null;
            float distance = float.MaxValue;

            int minX = Mathf.RoundToInt(position.x - range);
            int maxX = Mathf.RoundToInt(position.x + range);
            int minY = Mathf.RoundToInt(position.y - range);
            int maxY = Mathf.RoundToInt(position.y + range);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (map.IsInFoV(x, y) == false) continue;

                    var entity = map.GetBlockingEntity(x, y);

                    if (entity != null && entity != owner)
                    {
                        var fighter = entity.GetComponent<Fighter>();

                        if (fighter != null)
                        {
                            float d = Vector2.Distance(entity.Position, position);

                            if (d < distance)
                            {
                                distance = d;
                                target = fighter;
                            }
                        }
                    }
                }
            }

            return target;
        }
    }
}
