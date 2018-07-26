using Roguelike.Entities;

namespace Roguelike.Actions
{
    public class UseItemActionResult : IActionResult
    {
        public Item Item { get; set; }
        public string Message { get; set; }
        public bool Consumed { get; set; }

        public UseItemActionResult(string message)
        {
            Message = message;
        }

        public UseItemActionResult(Item item, bool consumed, string message)
        {
            Item = item;
            Consumed = consumed;
            Message = message;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
