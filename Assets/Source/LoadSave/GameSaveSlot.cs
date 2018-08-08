using UnityEngine;
using System;
using System.Collections.Generic;

namespace Roguelike.LoadSave
{
    [Serializable]
    public class GameSaveSlot
    {
        public GameMapData mapData;
        public VisibilityMapData visibilityData;
        public List<EntityData> entities;

        public GameSaveSlot()
        {
            entities = new List<EntityData>();
        }
    }
}
