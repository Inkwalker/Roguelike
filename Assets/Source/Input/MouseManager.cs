using Roguelike.Dungeon;
using Roguelike.Entities;
using Roguelike.Gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike
{
    public class MouseManager : MonoBehaviour
    {
        [SerializeField] new Camera camera;

        private MouseClickDetector leftMouseButton;

        public TileSelectedEvent TileSelected;
        public EntitySelectedEvent EntitySelected;

        private void Awake()
        {
            leftMouseButton = new MouseClickDetector(0);
            leftMouseButton.onButtonDown += OnClick;
        }

        private void Update()
        {
            leftMouseButton.Update();
        }

        private void OnClick(MouseClickDetector mouseButton)
        {
            if (mouseButton == leftMouseButton)
            {
                Vector3 mp = Input.mousePosition;
                Ray mouseRay = camera.ScreenPointToRay(mp);

                RaycastHit raycastHit;
                if (Physics.Raycast(mouseRay, out raycastHit))
                {
                    var tile = raycastHit.collider.gameObject.GetComponent<Tile>();

                    if (tile != null)
                    {
                        TileSelected.Invoke(tile);
                    }

                    var entity = raycastHit.collider.gameObject.GetComponent<Entity>();
                    if (entity != null)
                    {
                        EntitySelected.Invoke(entity);
                    }
                }
            }
        }

        [System.Serializable]
        public class TileSelectedEvent : UnityEvent<Tile> { }
        [System.Serializable]
        public class EntitySelectedEvent : UnityEvent<Entity> { }
    }
}
