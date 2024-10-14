using Unity.Entities;
using UnityEngine;

// Authoring component is a monobehavior and can be attached to a gameobject
// Connects the entity and game object together via baking
public class RotateSpeedAuthoring : MonoBehaviour
{
    private float _speed = 0;
    
    // Baker (turns monobehaviour into entity)
    private class Baker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                Speed = authoring._speed
            });
        }
    }
}

// Regular ECS component (cannot be attached to gameobject)
public struct RotateSpeed : IComponentData
{
    public float Speed;
}
