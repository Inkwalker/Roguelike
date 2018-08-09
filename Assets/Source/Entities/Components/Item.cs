using Roguelike.Actions;
using Roguelike.LoadSave;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Item : AEntityComponent
    {
        [SerializeField] GameObject icon;
        public GameObject Icon { get { return icon; } }

        [SerializeField] TargetMode targeting;
        public TargetMode Targeting { get { return targeting; } }

        [SerializeField] string targetingMessage;
        public string TargetingMessage { get { return targetingMessage; } }

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

        public override AEntityComponentData GetData()
        {
            return null;
        }

        public override void SetData(EntityInstanceData data)
        {
        }

        public enum TargetMode
        {
            Self,
            Target
        }
    }
}
