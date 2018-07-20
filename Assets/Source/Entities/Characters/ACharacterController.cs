using Roguelike.Gameplay;
using UnityEngine;

namespace Roguelike.Entities
{
    public abstract class ACharacterController : MonoBehaviour
    {
        public Character Character { get; private set; }

        protected virtual void Awake()
        {
            Character = GetComponent<Character>();
        }

        public abstract MoveState TakeTurn();
    }
}
