using Roguelike.Actions;
using Roguelike.Dungeon;
using Roguelike.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Character : EntityComponent
    {
        [SerializeField]
        private float moveSpeed = 1f;
        [SerializeField]
        private int viewDistance = 10;
        [SerializeField]
        private Animator animator;

        private GameMap map;
        private bool moving;

        private Fighter fighter;

        public int ViewDistance { get { return viewDistance; } }

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            map = FindObjectOfType<GameMap>();
        }

        public MoveState Move(Vector2Int position, int distance)
        {
            MoveState state = new MoveState();

            if (fighter.Dead == false)
            {
                if (moving == true)
                {
                    StopCoroutine(Move(Vector2.zero));
                }

                Tile tile = map.GetTile(position);

                if (tile.Blocked == false)
                {
                    var targetEntity = map.GetBlockingEntity(tile.Position.x, tile.Position.y);

                    var path = map.FindPath(Entity.Position, tile.Position, true);

                    if (path.Length > 0)
                    {
                        List<Tile> p = new List<Tile>();
                        for (int i = 0; i < distance + 1; i++)
                        {
                            if (i >= path.Length) break;

                            p.Add(path[i]);
                        }

                        if (p.Count == 1 && targetEntity != null)
                        {
                            InteractWithEntity(targetEntity, state);
                        }
                        else
                        {
                            StartCoroutine(MovePath(p.ToArray(), state));
                        }
                    }
                }
                else
                {
                    state.Results.Add(new InvalidActionResult());
                    state.Finished = true;
                }
            }
            else
            {
                state.Finished = true;
            }

            return state;
        }

        public MoveState PickItem(Item item)
        {
            MoveState moveState = new MoveState();

            var inventory = GetComponent<Inventory>();

            if (inventory != null)
            {
                var result = inventory.Add(item);
                moveState.Results.Add(result);
            }

            moveState.Finished = true;
            return moveState;
        }

        private void InteractWithEntity(Entity entity, MoveState state)
        {
            var fighter = GetComponent<Fighter>();
            var targetFighter = entity.GetComponent<Fighter>();

            Vector2 delta = entity.Position - GetComponent<Entity>().Position;
            transform.rotation = Quaternion.LookRotation(new Vector3(delta.x, transform.position.y, delta.y));

            if (targetFighter != null)
            {
                var attackResults = fighter.Attack(targetFighter);

                state.Results.AddRange(attackResults);
            }

            state.Finished = true;
        }

        private IEnumerator MovePath(Tile[] path, MoveState state)
        {
            var queue = new Queue<Tile>(path);

            queue.Dequeue(); //remove the first point. we are already here

            while(queue.Count > 0)
            {
                var tile = queue.Dequeue();

                Vector3 pos = transform.position;
                Vector3 target = tile.transform.position;

                Vector2 delta = new Vector2(
                    target.x - pos.x,
                    target.z - pos.z
                    );

                yield return Move(delta);
            }

            state.Finished = true;
        }

        private IEnumerator Move(Vector2 delta)
        {
            moving = true;
            animator.SetFloat("Speed", 1f);

            Vector2 start = new Vector2(transform.position.x, transform.position.z);
            Vector2 end = start + delta;
            float t = 0;

            float moveTime = delta.magnitude / moveSpeed;

            transform.rotation = Quaternion.LookRotation(new Vector3(delta.x, transform.position.y, delta.y)); 

            while (t <= 1)
            {
                var pos = Vector2.Lerp(start, end, t);

                Entity.WorldPosition = pos;

                t += Time.deltaTime / moveTime;
                yield return null;
            }

            animator.SetFloat("Speed", 0);
            moving = false;
        }
    }
}
