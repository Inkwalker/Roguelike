using UnityEngine;
using Roguelike.Entities;
using Roguelike.Dungeon;
using Roguelike.UI;
using Roguelike.Gameplay;

namespace Roguelike.GameStates
{
    public class PlayerTurnSubState : GameSubState
    {
        private InventoryWindow inventoryWindow;

        private PlayerController player;

        private MouseManager mouseManager;
        private GameMap gameMap;
        private MessageLog log;

        private MoveState activeMoveState;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();

            gameMap = FindObjectOfType<GameMap>();
            log = FindObjectOfType<MessageLog>();

            mouseManager = FindObjectOfType<MouseManager>();

            inventoryWindow = FindObjectOfType<InventoryWindow>();
        }

        public override void Activate()
        {
            base.Activate();

            mouseManager.TileSelected.AddListener(OnTileSelected);
            mouseManager.EntitySelected.AddListener(OnEntitySelected);

            inventoryWindow.ItemSelected.AddListener(OnInventoryItemSelected);
            inventoryWindow.ItemDropped.AddListener(OnInventoryItemDropped);
        }

        public override void Deactivate()
        {
            mouseManager.TileSelected.RemoveListener(OnTileSelected);
            mouseManager.EntitySelected.RemoveListener(OnEntitySelected);

            inventoryWindow.ItemSelected.RemoveListener(OnInventoryItemSelected);
            inventoryWindow.ItemDropped.RemoveListener(OnInventoryItemDropped);

            inventoryWindow.Hide();

            base.Deactivate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryWindow.gameObject.activeSelf)
                {
                    inventoryWindow.Hide();
                }
                else
                {
                    var inv = player.GetComponent<Inventory>();

                    inventoryWindow.Show(inv);
                }
            }

            //process player move results
            if (activeMoveState != null)
            {
                foreach (var result in activeMoveState.Results)
                {
                    log.AddMessage(result);
                }

                activeMoveState.Results.Clear();

                //next turn
                if (activeMoveState.Finished)
                {
                    gameMap.RecalulatePathfinding();

                    activeMoveState = null;

                    Deactivate();
                }
            }
        }

        private void OnTileSelected(Tile tile)
        {
            if (gameObject.activeSelf)
            {
                activeMoveState = player.MoveTo(tile);
            }
        }

        private void OnEntitySelected(Entity entity)
        {
            if (gameObject.activeSelf)
            {
                var item = entity.GetComponent<Item>();

                if (item != null)
                {
                    activeMoveState = player.PickItem(item);
                }
            }
        }

        private void OnInventoryItemSelected(Item item)
        {
            var inventory = player.GetComponent<Inventory>();

            if (inventory != null)
            {
                activeMoveState = new MoveState();
                activeMoveState.Results.AddRange(inventory.Use(item));
                activeMoveState.Finished = true;
            }

            inventoryWindow.Hide();
        }

        private void OnInventoryItemDropped(Item item)
        {
            var inventory = player.GetComponent<Inventory>();

            if (inventory != null)
            {
                activeMoveState = new MoveState();
                activeMoveState.Results.AddRange(inventory.Drop(item));
                activeMoveState.Finished = true;
            }

            inventoryWindow.Hide();
        }
    }
}
