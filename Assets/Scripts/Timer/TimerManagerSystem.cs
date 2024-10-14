using System;
using Unity.Entities;

public partial class TimerManagerSystem : SystemBase
{
    public event Action<float> OnTimeUpdate;

    protected override void OnCreate()
    {
        RequireForUpdate<TimerManager>();
    }
    
    protected override void OnUpdate()
    {
        foreach (RefRW<TimerManager> timeManager in SystemAPI.Query<RefRW<TimerManager>>())
        {
            timeManager.ValueRW.CurrentTime += SystemAPI.Time.DeltaTime;
            // ?. evaluates the first operand against null for safety
            OnTimeUpdate?.Invoke(timeManager.ValueRW.CurrentTime);
        }
    }
}
