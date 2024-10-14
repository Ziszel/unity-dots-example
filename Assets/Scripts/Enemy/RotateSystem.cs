using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

// ISystem is compatible with burst and generally faster
// SystemBase has more convenient features but requires garbage collection and increased SourcGen compile time
[BurstCompile]
public partial struct RotateSystem : ISystem 
{
    // Ensures system only runs if valid entities exist
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        // -- WITHOUT JOB ---
        // RefRO = read only (RefRW can be used when writing data is required)
        // references can be more optimised than working with copies
        // foreach ((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed)
        //          in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithNone<Player>())
        // {
        //     localTransform.ValueRW =
        //         localTransform.ValueRW.RotateX(rotateSpeed.ValueRO.Speed * SystemAPI.Time.DeltaTime);
        //     localTransform.ValueRW =
        //         localTransform.ValueRW.RotateZ(rotateSpeed.ValueRO.Speed * SystemAPI.Time.DeltaTime);
        // }

        // -- WITH JOB --
        RotateStarJob rotateStarJob = new RotateStarJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        // Job will run at some point in the future (Use Schedule over Run),
        // across multiple worker threads if required (Parallel)
        rotateStarJob.ScheduleParallel();
        
        // If using something other than IJobEntity (E.g., IJobFor, use the longhand syntax as below
        // Ensures dependencies are properly set
        // state.Dependency = rotateStarJob.Schedule(state.dependency);
    }
    
    [BurstCompile]
    [WithNone(typeof(Player))]
    public partial struct RotateStarJob : IJobEntity
    {
        public float deltaTime;
        // ref == RefRW, in == RefRO
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            localTransform =
                localTransform.RotateX(rotateSpeed.Speed * deltaTime);
            localTransform =
                localTransform.RotateZ(rotateSpeed.Speed * deltaTime);
        }
    }
}
