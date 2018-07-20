using System.Collections.Generic;
using Roguelike.Actions;

namespace Roguelike.Gameplay
{
    public class MoveState
    {
        public bool Finished { get; set; }

        public List<IActionResult> Results { get; private set; }

        public MoveState()
        {
            Results = new List<IActionResult>();
        }
    }
}
