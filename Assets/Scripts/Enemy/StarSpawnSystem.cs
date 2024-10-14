using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;

public partial struct StarSpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }
    
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<StarManager> starManager in SystemAPI.Query<RefRW<StarManager>>())
        {
            ProcessStarSpawn(ref state, starManager);
        }
    }

    private void ProcessStarSpawn(ref SystemState state, RefRW<StarManager> starManager)
    {
        if (starManager.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            Entity newEntity = state.EntityManager.Instantiate(starManager.ValueRO.Prefab);
            state.EntityManager.SetComponentData(newEntity,
                new LocalTransform
                {
                    Position = new float3(UnityEngine.Random.Range(-25, +25), 
                        UnityEngine.Random.Range(+10, +40), 
                        UnityEngine.Random.Range(-25, +25)),
                    Rotation = quaternion.identity,
                    Scale = 0.05f
                });
            state.EntityManager.SetComponentData(newEntity,
                new RotateSpeed
                {
                    Speed = GetRandomNumberWithoutZero()
                });
            starManager.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + starManager.ValueRO.SpawnRate;
        }
    }

    [BurstCompile]
    private int GetRandomNumberWithoutZero()
    {
        int returnValue = 0;
        while (returnValue == 0)
        {
            returnValue = UnityEngine.Random.Range(-10, 10);
        }

        return returnValue;
    }
}
