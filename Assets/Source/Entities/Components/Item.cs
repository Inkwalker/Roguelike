using Roguelike.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Item : EntityComponent
    {
        public IActionResult[] Use(Entity target)
        {
            var results = new List<IActionResult>();

            var functions = GetComponents<AItemFunction>();

            if (functions.Length == 0)
            {
                results.Add(new MessageActionResult(string.Format("The {0} cannot be used", Entity.DisplayName)));
            }
            else
            {
                foreach (var function in functions)
                {
                    var result = function.Execute(target);

                    results.AddRange(result);
                }
            }

            return results.ToArray();
        }
    }
}
