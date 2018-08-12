using Roguelike.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roguelike.GameStates
{
    public class MainMenuState : GameState
    {
        [SerializeField] string mainMenuScene = "MainMenu";

        [SerializeField] GameState loadGameState;
        [SerializeField] GameState newGameState;

        private MainMenu menu;

        public void LoadGame()
        {
            TransitionToState(loadGameState);
        }

        public void NewGame()
        {
            TransitionToState(newGameState);
        }

        private void OnEnable()
        {
            var menuScene = SceneManager.GetSceneByName(mainMenuScene);
            if (menuScene.IsValid() == false)
            {
                SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
            }

            StartCoroutine(FindRefs());
        }

        private void OnDisable()
        {
            if (menu != null)
            {
                menu.onLoadGame.RemoveListener(LoadGame);
                menu.onNewGame.RemoveListener(NewGame);
            }

            SceneManager.UnloadSceneAsync(mainMenuScene);
        }

        IEnumerator FindRefs()
        {
            yield return null;

            menu = FindObjectOfType<MainMenu>();

            if (menu != null)
            {
                menu.onLoadGame.AddListener(LoadGame);
                menu.onNewGame.AddListener(NewGame);
            }
        }
    }
}
