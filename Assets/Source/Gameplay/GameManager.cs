﻿using UnityEngine;
using System.Collections.Generic;
using Roguelike.Entities;
using Roguelike.Actions;
using Roguelike.UI;
using Roguelike.Dungeon;

namespace Roguelike.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        private GameState gameState;

        private GameMap gameMap;
        private MessageLog log;

        private PlayerController player;
        private BasicMonster[] enemies;

        private Queue<BasicMonster> moveQueue;
        private MoveState activeMoveState;

        private void Awake()
        {
            gameMap = FindObjectOfType<GameMap>();
            log = FindObjectOfType<MessageLog>();
        }

        private void Start()
        {
            gameMap.CreateMap();

            player = FindObjectOfType<PlayerController>();
            enemies = FindObjectsOfType<BasicMonster>();

            moveQueue = new Queue<BasicMonster>(enemies);
        }

        // Update is called once per frame
        void Update()
        {
            if (gameState == GameState.EnemyTurn)
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
                        gameState = GameState.PlayerTurn;
                        moveQueue = new Queue<BasicMonster>(enemies);
                    }
                }
            }
            else if (gameState == GameState.PlayerTurn)
            {
                //process player move results
                if (activeMoveState != null)
                {
                    foreach (var result in activeMoveState.Results)
                    {
                        log.AddMessage(result);
                    }

                    activeMoveState.Results.Clear();

                    //next turn
                    if (activeMoveState.Finished)
                    {
                        gameMap.RecalulatePathfinding();

                        activeMoveState = null;
                        gameState = GameState.EnemyTurn;
                    }
                }
            }
        }

        public void OnTileSelected(Tile tile)
        {
            if (gameState == GameState.PlayerTurn)
            {
                activeMoveState = player.MoveTo(tile);
            }
        }

        private enum GameState
        {
            PlayerTurn,
            EnemyTurn
        }
    }
}