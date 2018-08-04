using UnityEngine;

namespace Roguelike.GameStates
{
    public abstract class GameState : MonoBehaviour
    {
        protected void TransitionToState(GameState state)
        {
            gameObject.SetActive(false);
            state.gameObject.SetActive(true);
        }

        protected GameSubState[] GetActiveSubStates()
        {
            var subStates = GetComponentsInChildren<GameSubState>();

            var activeStates = System.Array.FindAll(subStates, (state) => { return state.gameObject.activeSelf; });

            return activeStates;
        }
    }
}
