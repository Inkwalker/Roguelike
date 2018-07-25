namespace Roguelike.Actions
{ 
    public class InvalidActionResult : IActionResult
    {
        public override string ToString()
        {
            return "Invalid action.";
        }
    }
}
