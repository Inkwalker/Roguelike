using UnityEngine;
using Roguelike.Entities;
using Roguelike.Dungeon;
using System.Collections.Generic;

namespace Roguelike.Actions
{
    public class FireballFunction : AItemFunction
    {
        [SerializeField] int radius = 2;
        [SerializeField] int damage = 1;

        public override IActionResult[] Execute(Entity target)
        {
            var results = new List<IActionResult>();

            var map = FindObjectOfType<GameMap>();
            var position = target.Position;

            if (map.IsInFoV(position.x, position.y) == false)
            {
                results.Add(new UseItemActionResult(Item, false, "You cannot target a tile outside your field of view.")); //yellow
                return results.ToArray();
            }

            results.Add(new UseItemActionResult(Item, true,
                string.Format("The fireball explodes, burning everything within {0} tiles!", radius)) //orange
            );

            int minX = Mathf.RoundToInt(position.x - radius);
            int maxX = Mathf.RoundToInt(position.x + radius);
            int minY = Mathf.RoundToInt(position.y - radius);
            int maxY = Mathf.RoundToInt(position.y + radius);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    var entity = map.GetBlockingEntity(x, y);

                    if (entity != null)
                    {
                        var fighter = entity.GetComponent<Fighter>();

                        if (fighter != null)
                        {
                            float d = Vector2.Distance(entity.Position, position);

                            if (d <= radius)
                            {
                                results.Add(new MessageActionResult(
                                    string.Format("The {0} gets burned for {1} hit points.", entity.DisplayName, damage) //orange
                                    ));
                                results.AddRange(fighter.TakeDamage(damage));
                            }
                        }
                    }
                }
            }

            return results.ToArray();
        }
    }
}
