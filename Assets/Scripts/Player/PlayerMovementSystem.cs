using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }
    
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRO<MovementData> movementData, RefRO<InputComponent> inputComponent,
                     RefRW<LocalTransform> localTransform)
                 in SystemAPI.Query<RefRO<MovementData>, RefRO<InputComponent>, RefRW<LocalTransform>>())
        {
            float xInput = inputComponent.ValueRO.move.x;
            float zInput = inputComponent.ValueRO.move.y;
            
            // position
            float3 position = localTransform.ValueRW.Position;
            position.x += xInput * movementData.ValueRO.Speed * SystemAPI.Time.DeltaTime;
            position.z += zInput * movementData.ValueRO.Speed * SystemAPI.Time.DeltaTime;
            localTransform.ValueRW.Position = position;
            
            // rotation
            localTransform.ValueRW.Rotation = quaternion.LookRotation(position, new float3(0, 1, 0));
        }
    }
}
