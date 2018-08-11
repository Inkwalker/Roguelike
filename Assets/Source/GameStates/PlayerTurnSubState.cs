using UnityEngine;
using Roguelike.Entities;
using Roguelike.Dungeon;
using Roguelike.UI;
using Roguelike.Gameplay;

namespace Roguelike.GameStates
{
    public class PlayerTurnSubState : GameSubState
    {
        [SerializeField] TargetingSubState targetingSubState;

        private InventoryWindow inventoryWindow;

        private PlayerController player;

        private MouseManager mouseManager;
        private GameMap gameMap;
        private MessageLog log;

        private MoveState activeMoveState;
        private Item selectedItem;

        private PlayerController Player
        {
            get
            {
                if (player == null)
                    player = FindObjectOfType<PlayerController>();

                return player;
            }
        }

        private void Awake()
        {
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

            targetingSubState.TargetSelected.AddListener(OnTargetSelected);
        }

        public override void Deactivate()
        {
            mouseManager.TileSelected.RemoveListener(OnTileSelected);
            mouseManager.EntitySelected.RemoveListener(OnEntitySelected);

            inventoryWindow.ItemSelected.RemoveListener(OnInventoryItemSelected);
            inventoryWindow.ItemDropped.RemoveListener(OnInventoryItemDropped);

            targetingSubState.TargetSelected.RemoveListener(OnTargetSelected);

            inventoryWindow.Hide();
            targetingSubState.Deactivate();

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
                    var inv = Player.GetComponent<Inventory>();

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
            if (targetingSubState.Active == false)
            {
                activeMoveState = Player.MoveTo(tile);
            }
        }

        private void OnEntitySelected(Entity entity)
        {
            if (targetingSubState.Active == false)
            {
                var item = entity.GetComponent<Item>();

                if (item != null)
                {
                    activeMoveState = Player.PickItem(item);
                }
            }
        }

        private void OnInventoryItemSelected(Item item)
        {
            if (targetingSubState.Active == false)
            {
                var inventory = Player.GetComponent<Inventory>();

                if (inventory != null)
                {
                    if (item.Targeting == Item.TargetMode.Self)
                    {
                        activeMoveState = new MoveState();
                        activeMoveState.Results.AddRange(inventory.Use(item, Player.GetComponent<Entity>()));
                        activeMoveState.Finished = true;
                    }
                    else
                    {
                        selectedItem = item;
                        targetingSubState.Activate();
                    }
                }

                inventoryWindow.Hide();
            }
        }

        private void OnInventoryItemDropped(Item item)
        {
            if (targetingSubState.Active == false)
            {
                var inventory = Player.GetComponent<Inventory>();

                if (inventory != null)
                {
                    activeMoveState = new MoveState();
                    activeMoveState.Results.AddRange(inventory.Drop(item));
                    activeMoveState.Finished = true;
                }

                inventoryWindow.Hide();
            }
        }

        private void OnTargetSelected(Tile target)
        {
            if (target != null && selectedItem != null)
            {
                var inventory = Player.GetComponent<Inventory>();

                var entity = gameMap.GetBlockingEntity(target.Position.x, target.Position.y);

                if (entity != null)
                {
                    activeMoveState = new MoveState();
                    activeMoveState.Results.AddRange(inventory.Use(selectedItem, entity));
                    activeMoveState.Finished = true;
                }
            }

            selectedItem = null;
        }
    }
}
