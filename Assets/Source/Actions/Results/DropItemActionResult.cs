using Roguelike.Entities;

namespace Roguelike.Actions
{
    public class DropItemActionResult : IActionResult
    {
        public Item Item { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
