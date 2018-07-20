using Roguelike.Dungeon;
using Roguelike.Gameplay;

namespace Roguelike.Entities
{
    public class PlayerController : ACharacterController
    {
        Entity entity;
        GameMap map;

        protected override void Awake()
        {
            base.Awake();

            entity = GetComponent<Entity>();
            entity.Moved.AddListener(HandlePositionChange);

            map = FindObjectOfType<GameMap>();
        }

        public override MoveState TakeTurn()
        {
            MoveState move = new MoveState();
            move.Finished = true;

            return move;
        }

        public MoveState MoveTo(Tile tile)
        {
            return Character.Move(tile.Position, 5);
        }

        public void HandlePositionChange(Entity entity)
        {
            map.UpdateVisibility(entity.Position, Character.ViewDistance);
        }
    }
}
