using Roguelike.Entities;

namespace Roguelike.Actions
{
    public class DeadActionResult : IActionResult
    {
        public Entity Target { get; private set; }

        public DeadActionResult(Entity entity)
        {
            Target = entity;
        }

        public override string ToString()
        {
            return Target.DisplayName + " is dead.";
        }
    }
}