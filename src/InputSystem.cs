using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Input System that modifies the Input State with the red Input
public class InputSystem : InputModifier
{
    private InputState state;

    public override void Process(ref InputState state)
    {
        state = this.state;
    }

    public void Move(InputAction.CallbackContext context)
    {
        state.MovementDirection = context.ReadValue<Vector2>();
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }
}