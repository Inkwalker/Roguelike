using UnityEngine;
using Roguelike.Dungeon;

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

        private void OnStateDeactivated(GameSubState subState)
        {
            if (subState == enemyTurnState)
                playerTurnState.Activate();

            if (subState == playerTurnState)
                enemyTurnState.Activate();
        }
    }
}
