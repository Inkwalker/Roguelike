using UnityEngine;
using System.Collections.Generic;
using System;

namespace Roguelike.LoadSave
{
    [Serializable]
    public class EntityInstanceData
    {
        private string instanceID;
        private string prefabID;
        private int positionX;
        private int positionY;
        private List<AEntityComponentData> components;

        public string PrefabID
        {
            get { return prefabID; }
        }

        public Guid InstanceID
        {
            get
            {
                return new Guid(instanceID);
            }
        }

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

        public EntityInstanceData(Guid instanceID, string prefabID)
        {
            this.prefabID = prefabID;
            this.instanceID = instanceID.ToString();

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
