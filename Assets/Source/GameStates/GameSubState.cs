using UnityEngine;
using UnityEngine.Events;

namespace Roguelike.GameStates
{
    public abstract class GameSubState : MonoBehaviour
    {
        public bool Active { get { return gameObject.activeSelf; } }

        public GameSubStateEvent Activated;
        public GameSubStateEvent Deactivated;

        public virtual void Activate()
        {
            gameObject.SetActive(true);

            Activated.Invoke(this);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);

            Deactivated.Invoke(this);
        }

        [System.Serializable]
        public class GameSubStateEvent : UnityEvent<GameSubState> { }
    }
}
