using UnityEngine;
using Roguelike.Dungeon;
using Roguelike.LoadSave;

namespace Roguelike.GameStates
{
    public class GameplayState : GameState
    {
        [SerializeField] GameSubState enemyTurnState;
        [SerializeField] GameSubState playerTurnState;

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
            gameMap.CreateMap();
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

                var mapData = gameMap.GetMapData();
                var visibilityData = gameMap.GetVisibilityData();

                slot.mapData = mapData;
                slot.visibilityData = visibilityData;

                var entities = gameMap.GetAllEntities();

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

                Debug.Log(slot.entities[0].Position);
                Debug.Log(slot.entities[0].GetComponentData<FighterComponentData>().HP);
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
