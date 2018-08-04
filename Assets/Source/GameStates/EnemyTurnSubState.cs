using Roguelike.Actions;
using Roguelike.Dungeon;
using Roguelike.Entities;
using Roguelike.Gameplay;
using Roguelike.UI;
using System.Collections.Generic;

namespace Roguelike.GameStates
{
    public class EnemyTurnSubState : GameSubState
    {
        private BasicMonster[] enemies;

        private Queue<BasicMonster> moveQueue;
        private MoveState activeMoveState = null;

        private MessageLog log;
        private GameMap gameMap;

        private void Awake()
        {
            log = FindObjectOfType<MessageLog>();
            gameMap = FindObjectOfType<GameMap>();
        }

        private void OnEnable()
        {
            enemies = FindObjectsOfType<BasicMonster>();

            moveQueue = new Queue<BasicMonster>(enemies);
        }

        private void Update()
        {
            //process ai move results
            if (activeMoveState != null)
            {
                foreach (var result in activeMoveState.Results)
                {
                    if (result is MessageActionResult)
                    {
                        log.AddMessage(result);
                    }
                    else if (result is DeadActionResult)
                    {
                        //player is dead!
                        log.AddMessage(new MessageActionResult("You died!"));
                    }
                }

                activeMoveState.Results.Clear();

                if (activeMoveState.Finished) activeMoveState = null;
            }
            //next move
            else
            {
                gameMap.RecalulatePathfinding();

                if (moveQueue.Count > 0)
                {
                    var enemy = moveQueue.Dequeue();
                    activeMoveState = enemy.TakeTurn();
                }
                else
                {
                    Deactivate(); //all enemies have moved
                }
            }
        }
    }
}
