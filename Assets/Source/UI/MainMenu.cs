using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Roguelike.UI
{
    public class MainMenu : MonoBehaviour
    {
        public UnityEvent onLoadGame;
        public UnityEvent onNewGame;

        public void OnContinueButton()
        {
            onLoadGame.Invoke();
        }

        public void OnNewGameButton()
        {
            onNewGame.Invoke();
        }

        public void OnExitButton()
        {
            Application.Quit();
        }
    }
}
