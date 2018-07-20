using Roguelike.Dungeon;
using Roguelike.Gameplay;
using UnityEngine;

namespace Roguelike
{
    public class MouseManager : MonoBehaviour
    {
        [SerializeField] new Camera camera;

        private GameManager gameManager;

        public MouseManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            //left mouse click
            if (Input.GetMouseButtonDown(0))
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
                }
            }
        }
    }
}
