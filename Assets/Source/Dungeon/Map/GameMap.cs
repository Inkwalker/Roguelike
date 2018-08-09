using UnityEngine;
using System.Collections;
using Autotiles;
using Roguelike.Pathfinding;
using Roguelike.Entities;
using Roguelike.Dungeon.Generator;
using Roguelike.LoadSave;
using System.Collections.Generic;

namespace Roguelike.Dungeon
{
    public class GameMap : MonoBehaviour
    {
        public static readonly float TileSize = 2f;

        [SerializeField] Brush wallBrush;
        [SerializeField] Brush floorBrush;
        [SerializeField] Material fovMaterial;

        [SerializeField] DungeonLevelSettings levelSettings;

        private Vector2Int size;
        private Tile[,] tiles;
        private EntityMap entityMap;
        private Tilemap tilemap;
        private VisibilityMap visibilityMap;
        private TileGridNodes pathfindingNodes;

        public int Width { get { return size.x; } }
        public int Height { get { return size.y; } }

        private void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }

        public void CreateMap(GameMapData data)
        {
            size = new Vector2Int(data.Width, data.Height);

            tilemap.Resize(size.x, size.y);

            tiles = new Tile[size.x, size.y];
            visibilityMap = new VisibilityMap(size.x, size.y);

            DestroyEntites();
            entityMap = new EntityMap(size.x, size.y);

            fovMaterial.SetVector("_FogSize", new Vector4(Width * TileSize, Height * TileSize, 0, 0));
            fovMaterial.SetTexture("_FogMapA", visibilityMap.texturePrev);
            fovMaterial.SetTexture("_FogMapB", visibilityMap.textureCur);

            //create tiles
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var brush = data.Get(x, y) == 0 ? floorBrush : wallBrush;

                    tilemap.SetTile(x, y, brush);
                }
            }

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    tiles[x, y] = tilemap.GetTileInstance(x, y).GetComponent<Tile>();
                }
            }

            //setup the character
            //var character = FindObjectOfType<PlayerController>();
            //var ce = character.GetComponent<Entity>();
            //ce.Position = dungeon.playerPosition;
            //entityMap.Add(ce);

            pathfindingNodes = new TileGridNodes(this);

            RecalulatePathfinding();
        }

        public void CreateEntities(List<EntityInstanceData> entities)
        {
            DestroyEntites();

            var entityDatabase = FindObjectOfType<EntityDatabase>();

            foreach (var entityData in entities)
            {
                var prefab = entityDatabase.GetPrefab(entityData.PrefabID);
                var entity = Instantiate(prefab);

                entity.SetData(entityData);

                entityMap.Add(entity);
            }
        }

        public GameMapData GetMapData()
        {
            GameMapData data = new GameMapData(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int tile = tilemap.GetTile(x, y) == floorBrush ? 0 : 1;

                    data.Set(x, y, tile);
                }
            }

            return data;
        }

        public VisibilityMapData GetVisibilityData()
        {
            VisibilityMapData data = new VisibilityMapData(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    data.Set(x, y,
                        visibilityMap.Get(x, y) == VisibilityMap.VisibilityState.Explored ||
                        visibilityMap.Get(x, y) == VisibilityMap.VisibilityState.Visible);
                }
            }

            return data;
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return null;

            return tiles[x, y];
        }

        public Tile GetTile(Vector2Int pos)
        {
            return GetTile(pos.x, pos.y);
        }

        public Entity GetTopEntity(int x, int y)
        {
            return entityMap.GetTop(new Vector2Int(x, y));
        }

        public Entity GetBlockingEntity(int x, int y)
        {
            return entityMap.GetBlocking(new Vector2Int(x, y));
        }

        public Entity[] GetAllEntities()
        {
            return entityMap.GetAll();
        }

        public void AddEntity(Entity entity)
        {
            entityMap.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entityMap.Remove(entity);
        }

        public void UpdateVisibility(Vector2Int pos, int distance)
        {
            visibilityMap.CopyToTexturePrev();
            visibilityMap.Reset();
            FieldOfView.UpdateVisibilityMap(this, visibilityMap, pos, distance);
            StartCoroutine(AnimateFoV());
        }

        public bool IsInFoV(int x, int y)
        {
            return visibilityMap.Get(x, y) == VisibilityMap.VisibilityState.Visible;
        }

        public bool IsBlocked(int x, int y)
        {
            var tile = GetTile(x, y);
            if (tile != null && tile.Blocked) return true;

            var entity = GetBlockingEntity(x, y);
            if (entity != null && entity.Blocks) return true;

            return false;
        }

        public Tile[] FindPath(Vector2Int from, Vector2Int to, bool returnClosest)
        {
            var fromNode = pathfindingNodes.GetNode(GetTile(from));
            var toNode = pathfindingNodes.GetNode(GetTile(to));

            var pathNodes = Pathfinder.FindPath<TileNode>(fromNode, toNode, returnClosest);

            Tile[] path = new Tile[pathNodes.Count];

            for (int i = 0; i < pathNodes.Count; i++)
            {
                path[i] = pathNodes[i].Tile;
            }

            return path;
        }

        public void RecalulatePathfinding()
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    pathfindingNodes.UpdateLinksForTile(GetTile(x, y));
                }
            }
        }

        IEnumerator AnimateFoV()
        {
            float time = 0.15f;
            fovMaterial.SetFloat("_FogMapBlend", 0);

            float t = 0;

            while (t < 1f)
            {
                fovMaterial.SetFloat("_FogMapBlend", Mathf.Clamp01(t));
                t += Time.deltaTime / time;
                yield return null;
            }

            fovMaterial.SetFloat("_FogMapBlend", 1);
        }

        private void DestroyEntites()
        {
            if (entityMap != null)
            {
                var oldInstances = entityMap.GetAll();

                foreach (var item in oldInstances)
                {
                    entityMap.Remove(item);
                    Destroy(item.gameObject);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (visibilityMap == null) return;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (visibilityMap.Get(x, y) == VisibilityMap.VisibilityState.Visible)
                    {
                        Gizmos.DrawCube(new Vector3(x * TileSize + TileSize * 0.5f, 0, y * TileSize + TileSize * 0.5f), new Vector3(2, 0.2f, 2));
                    }
                }
            }
        }
    }
}
