using Unity.Entities;
using UnityEngine;

public class TimerManagerAuthoring : MonoBehaviour
{
    private class Baker : Baker<TimerManagerAuthoring>
    {
        public override void Bake(TimerManagerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TimerManager()
            {
                // current time will ALWAYS start as 0
                CurrentTime = 0.0f
            });
        }
    }
    
}

public struct TimerManager : IComponentData
{
    public float CurrentTime;
}
