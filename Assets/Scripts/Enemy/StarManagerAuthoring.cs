using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class StarManagerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;
    public int AmountToSpawn;
    public int NextSpawnTime;
    private class Baker : Baker<StarManagerAuthoring>
    {
        public override void Bake(StarManagerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new StarManager
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnRate = authoring.SpawnRate,
                NextSpawnTime = authoring.SpawnRate,
                AmountToSpawn = authoring.AmountToSpawn
            });
        }
    }
}

public struct StarManager : IComponentData
{
    public Entity Prefab;
    public float SpawnRate;
    public float NextSpawnTime;
    public int AmountToSpawn;
}
