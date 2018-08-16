﻿using UnityEngine;

namespace Roguelike.UI
{
    public class GameplayUI : MonoBehaviour
    {
        private static GameplayUI instance;
        public static GameplayUI Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<GameplayUI>();

                return instance;
            }
        }

        [SerializeField] InventoryWindow inventoryWindow;
        [SerializeField] TargetingMessageWindow targetingWindow;
        [SerializeField] GameOverWindow gameOverWindow;

        public InventoryWindow InventoryWindow { get { return inventoryWindow; } }
        public TargetingMessageWindow TargetingWindow { get { return targetingWindow; } }
        public GameOverWindow GameOverWindow { get { return gameOverWindow; } }

        public void HideAll()
        {
            InventoryWindow.Hide();
            TargetingWindow.Hide();
            GameOverWindow.Hide();
        }
    }
}
