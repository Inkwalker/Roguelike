using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Roguelike.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] GameObject content;
        [SerializeField] Button exitButton;

        public UnityEvent OnExit;

        private void Awake()
        {
            exitButton.onClick.AddListener(OnExitButton);
        }

        public void Show()
        {
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }

        private void OnExitButton()
        {
            OnExit.Invoke();
        }
    }
}
