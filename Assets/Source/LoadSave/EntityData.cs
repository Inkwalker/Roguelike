using UnityEngine;
using System.Collections.Generic;
using System;

namespace Roguelike.LoadSave
{
    [Serializable]
    public class EntityData
    {
        private int positionX;
        private int positionY;
        private List<AEntityComponentData> components;

        public Vector2Int Position
        {
            get
            {
                return new Vector2Int(positionX, positionY);
            }
            set
            {
                positionX = value.x;
                positionY = value.y;
            }
        }

        public EntityData()
        {
            components = new List<AEntityComponentData>();
        }

        public void AddComponentData(AEntityComponentData data)
        {
            components.Add(data);
        }

        public T GetComponentData<T>() where T : AEntityComponentData
        {
            var componentData = components.Find((data) => data is T);

            return componentData as T;
        }

        [Serializable]
        public class ComponentsDictionarty : Dictionary<string, object> { }
    }
}
