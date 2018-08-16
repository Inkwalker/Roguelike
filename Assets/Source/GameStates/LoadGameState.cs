using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Roguelike.Dungeon;
using Roguelike.LoadSave;

namespace Roguelike.GameStates
{
    public class LoadGameState : GameState
    {
        [SerializeField] string gameplayScene;
        [SerializeField] GameState gameplayState;

        private void OnEnable()
        {
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            var scene = SceneManager.GetSceneByName(gameplayScene);

            if (scene.IsValid())
            {
                var o = SceneManager.UnloadSceneAsync(scene);
                yield return o;
            }

            SceneManager.LoadScene(gameplayScene, LoadSceneMode.Additive);

            yield return null; //wait until all GameObjects are initialized

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(gameplayScene));

            var gameMap = FindObjectOfType<GameMap>();

            var storage = new GameSaveStorage();
            var slot = storage.Read();

            gameMap.CreateMap(slot.mapData);
            gameMap.CreateEntites(slot.entities);

            yield return null;

            TransitionToState(gameplayState);
        }
    }
}
