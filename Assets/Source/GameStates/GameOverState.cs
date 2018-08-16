using UnityEngine;
using Roguelike.UI;
using UnityEngine.SceneManagement;

namespace Roguelike.GameStates
{
    public class GameOverState : GameState
    {
        [SerializeField] string gameplayScene;
        [SerializeField] GameState mainMenuState;

        private void OnEnable()
        {
            GameplayUI.Instance.GameOverWindow.OnExit.AddListener(Exit);
            GameplayUI.Instance.GameOverWindow.Show();
        }

        private void OnDisable()
        {
            GameplayUI.Instance.GameOverWindow.Hide();
            GameplayUI.Instance.GameOverWindow.OnExit.RemoveListener(Exit);
        }

        private void Exit()
        {
            SceneManager.UnloadSceneAsync(gameplayScene);

            TransitionToState(mainMenuState);
        }
    }
}
