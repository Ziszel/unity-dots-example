using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float Speed;
    public class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Player{});
            AddComponent(entity, new MovementData()
            {
                Speed = authoring.Speed
            });
            AddComponent(entity, new InputComponent{});
        }
    }
}

public struct MovementData : IComponentData
{
    public float Speed;
}

public struct Player : IComponentData
{
   
}
