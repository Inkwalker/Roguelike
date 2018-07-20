using Roguelike.Dungeon;
using Roguelike.Gameplay;

namespace Roguelike.Entities
{
    public class BasicMonster : ACharacterController
    {
        GameMap map;
        Fighter fighter;

        protected override void Awake()
        {
            base.Awake();

            map = FindObjectOfType<GameMap>();
            fighter = GetComponent<Fighter>();
        }

        public override MoveState TakeTurn()
        {
            MoveState state = new MoveState();

            if (fighter.Dead)
            {
                state.Finished = true;
                return state;
            }

            var entity = GetComponent<Entity>();

            if (map.IsInFoV(entity.Position.x, entity.Position.y))
            {
                PlayerController player = FindObjectOfType<PlayerController>();
                Entity target = player.GetComponent<Entity>();

                state = Character.Move(target.Position, 2);
            }
            else
            {
                state.Finished = true;
            }

            return state;
        }
    }
}
