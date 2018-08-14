using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Roguelike.Dungeon;
using Roguelike.Entities;
using Roguelike.UI;

namespace Roguelike.GameStates
{
    public class TargetingSubState : GameSubState
    {
        private TargetingMessageWindow targetingMessage;

        private MouseManager mouseManager;

        public TargetSelectedEvent TargetSelected;

        private void Awake()
        {
            mouseManager = FindObjectOfType<MouseManager>();
        }

        public override void Activate()
        {
            base.Activate();

            targetingMessage = FindObjectOfType<TargetingMessageWindow>();
            targetingMessage.Show();

            mouseManager.TileSelected.AddListener(OnTileSelected);
            mouseManager.EntitySelected.AddListener(OnEntitySelected);
        }

        public override void Deactivate()
        {
            if (Active)
            {
                mouseManager.TileSelected.RemoveListener(OnTileSelected);
                mouseManager.EntitySelected.RemoveListener(OnEntitySelected);

                targetingMessage.Hide();
            }

            base.Deactivate();
        }

        private void OnTileSelected(Tile tile)
        {
            TargetSelected.Invoke(tile);

            Deactivate();
        }

        private void OnEntitySelected(Entity entity)
        {
            var map = FindObjectOfType<GameMap>();
            var tile = map.GetTile(entity.Position);

            TargetSelected.Invoke(tile);

            Deactivate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TargetSelected.Invoke(null);
                Deactivate();
            }
        }

        [System.Serializable]
        public class TargetSelectedEvent : UnityEvent<Tile> { }
    }
}
