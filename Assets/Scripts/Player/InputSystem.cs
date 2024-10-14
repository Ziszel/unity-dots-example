using Unity.Entities;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    private PlayerInput Input = null;
    
    protected override void OnCreate()
    {
        Input = new PlayerInput();
        Input.Enable();
    }
    
    protected override void OnUpdate()
    {
        foreach (RefRW<InputComponent> inputData in SystemAPI.Query<RefRW<InputComponent>>())
        {
            inputData.ValueRW.move = Input.Player.Move.ReadValue<Vector2>();
        }
    }
    
    
}
