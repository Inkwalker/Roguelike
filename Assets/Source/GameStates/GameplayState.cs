using UnityEngine;
using Roguelike.Dungeon;
using Roguelike.LoadSave;
using Roguelike.Entities;
using Roguelike.UI;

namespace Roguelike.GameStates
{
    public class GameplayState : GameState
    {
        [SerializeField] EnemyTurnSubState enemyTurnState;
        [SerializeField] PlayerTurnSubState playerTurnState;
        [SerializeField] GameState gameOverState;

        private GameMap gameMap;

        private void Awake()
        {
            enemyTurnState.OnEnemyTurnEnded.AddListener(OnTurnEnded);
            enemyTurnState.OnPlayerDead.AddListener(OnPlayerDead);
            playerTurnState.PlayerTurnEnded.AddListener(OnTurnEnded);
        }

        private void OnEnable()
        {
            gameMap = FindObjectOfType<GameMap>();

            var character = FindObjectOfType<PlayerController>();
            GameplayUI.Instance.HPBar.Character = character.GetComponent<Fighter>();

            playerTurnState.Activate();
        }

        private void Update()
        {
            //Save game
            if (Input.GetKeyDown(KeyCode.F5))
            {
                var storage = new GameSaveStorage();
                var slot = new GameSaveSlot();

                var entityDatabase = FindObjectOfType<EntityDatabase>();

                var mapData = gameMap.GetMapData();
                var visibilityData = gameMap.GetVisibilityData();

                slot.mapData = mapData;
                slot.visibilityData = visibilityData;

                slot.entities = new EntitiesData();
                var entities = entityDatabase.GetAllInstances();

                foreach (var entity in entities)
                {
                    var entityData = entity.GetData();

                    slot.entities.Add(entityData);
                }

                storage.Write(slot);

                var log = FindObjectOfType<UI.MessageLog>();
                log.AddMessage(new Actions.MessageActionResult("Game saved."));
            }

            //Load game
            //if (Input.GetKeyDown(KeyCode.F6))
            //{
            //    var storage = new GameSaveStorage();
            //    var slot = storage.Read();

            //    gameMap.CreateMap(slot.mapData);

            //    CreateEntites(slot.entities);
            //}
        }

        private void OnPlayerDead(GameSubState subState)
        {
            playerTurnState.Deactivate();
            enemyTurnState.Deactivate();

            TransitionToState(gameOverState);
        }

        private void OnTurnEnded(GameSubState subState)
        {
            if (subState == enemyTurnState)
            {
                playerTurnState.Activate();
                enemyTurnState.Deactivate();
            }

            if (subState == playerTurnState)
            {
                enemyTurnState.Activate();
                playerTurnState.Deactivate();
            }
        }
    }
}
