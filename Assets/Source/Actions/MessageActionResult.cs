namespace Roguelike.Actions
{
    public class MessageActionResult : IActionResult
    {
        public string Message { get; private set; }

        public MessageActionResult(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
