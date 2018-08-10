using System;

namespace Roguelike.LoadSave
{
    [Serializable]
    public class GameSaveSlot
    {
        public GameMapData mapData;
        public VisibilityMapData visibilityData;
        public EntitiesData entities;
    }
}
