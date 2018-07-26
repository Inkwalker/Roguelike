using Roguelike.Dungeon;
using Roguelike.Entities;
using Roguelike.Gameplay;
using UnityEngine;

namespace Roguelike
{
    public class MouseManager : MonoBehaviour
    {
        [SerializeField] new Camera camera;

        private GameManager gameManager;

        private MouseClickDetector leftMouseButton;

        private void Awake()
        {
            leftMouseButton = new MouseClickDetector(0);
            leftMouseButton.onButtonDown += OnClick;

            gameManager = FindObjectOfType<GameManager>();
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
                        gameManager.OnTileSelected(tile);
                    }

                    var item = raycastHit.collider.gameObject.GetComponent<Item>();
                    if (item != null)
                    {
                        gameManager.OnItemSelected(item);
                    }
                }
            }
        }
    }
}
