using UnityEngine;

namespace Roguelike.UI
{
    public class TargetingMessageWindow : MonoBehaviour
    {
        [SerializeField] GameObject content;

        public bool Visible { get { return content.activeSelf; } }

        public void Show()
        {
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }
    }
}
