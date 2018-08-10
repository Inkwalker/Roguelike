using UnityEngine;
using Roguelike.Dungeon;
using Roguelike.LoadSave;
using Roguelike.Dungeon.Generator;
using Roguelike.Entities;

namespace Roguelike.GameStates
{
    public class GameplayState : GameState
    {
        [SerializeField] GameSubState enemyTurnState;
        [SerializeField] GameSubState playerTurnState;

        [SerializeField] DungeonLevelSettings generatorSettings;

        private GameMap gameMap;

        private void Awake()
        {
            gameMap = FindObjectOfType<GameMap>();

            enemyTurnState.Deactivate();
            playerTurnState.Deactivate();

            enemyTurnState.Deactivated.AddListener(OnStateDeactivated);
            playerTurnState.Deactivated.AddListener(OnStateDeactivated);
        }

        private void Start()
        {
            var dungeonData = DungeonGenerator.Generate(generatorSettings);

            var mapData = dungeonData.GetGameMapData();
            var entitiesData = dungeonData.GetEntitiesData();

            gameMap.CreateMap(mapData);

            var entityDatabase = FindObjectOfType<EntityDatabase>();
            foreach (var data in entitiesData)
            {
                var entity = entityDatabase.CreateInstance(data);
                gameMap.AddEntity(entity);
            }
        }

        private void OnEnable()
        {
            playerTurnState.Activate();
        }

        private void Update()
        {
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
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                var storage = new GameSaveStorage();
                var slot = storage.Read();

                gameMap.CreateMap(slot.mapData);

                var entityDatabase = FindObjectOfType<EntityDatabase>();
                foreach (var data in slot.entities)
                {
                    var entity = entityDatabase.CreateInstance(data);
                    gameMap.AddEntity(entity);
                }
            }
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
