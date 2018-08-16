using Roguelike.Dungeon;
using Roguelike.Dungeon.Generator;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roguelike.GameStates
{
    public class NewGameState : GameState
    {
        [SerializeField] string gameplayScene;
        [SerializeField] GameState gameplayState;

        [SerializeField] DungeonLevelSettings generatorSettings;

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

            var dungeonData = DungeonGenerator.Generate(generatorSettings);

            var mapData = dungeonData.GetGameMapData();
            var entitiesData = dungeonData.GetEntitiesData();

            gameMap.CreateMap(mapData);
            gameMap.CreateEntites(entitiesData);

            yield return null;

            TransitionToState(gameplayState);
        }
    }
}
