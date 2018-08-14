using UnityEngine;
using Roguelike.Dungeon;
using Roguelike.LoadSave;
using Roguelike.Entities;

namespace Roguelike.GameStates
{
    public class GameplayState : GameState
    {
        [SerializeField] GameSubState enemyTurnState;
        [SerializeField] GameSubState playerTurnState;

        private GameMap gameMap;

        private void Awake()
        {
            enemyTurnState.Deactivate();
            playerTurnState.Deactivate();

            enemyTurnState.Deactivated.AddListener(OnStateDeactivated);
            playerTurnState.Deactivated.AddListener(OnStateDeactivated);
        }

        private void Start()
        {
            gameMap = FindObjectOfType<GameMap>();
        }

        private void OnEnable()
        {
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

        private void OnStateDeactivated(GameSubState subState)
        {
            if (subState == enemyTurnState)
                playerTurnState.Activate();

            if (subState == playerTurnState)
                enemyTurnState.Activate();
        }
    }
}
