using System;
using UnityEngine;

// Provides the InputState and lets external Input Middleware modify the Input State
// Designed after the Chain-of-Responsibility Pattern
//      -> Reference: https://www.dofactory.com/net/chain-of-responsibility-design-pattern
// Reference to Original Design Concept from Arthifical: https://www.youtube.com/watch?v=pOEyYwKtHJo
public class InputProvider : MonoBehaviour
{
    // All active Modifiers for the Input
    // Gets processed from lowest to highest
    [SerializeField] private InputModifier[] InputModifier;

    public Action<Vector2> OnMove = delegate(Vector2 direction) { };

    private void Awake()
    {
        // Subscribe to all relevant Events of all Input Modifiers
        foreach (var modifier in InputModifier)
        {
            modifier.OnMove += Move;
        }
    }

    private void OnDestroy()
    {
        // Subscribe to all relevant Events of all Input Modifiers
        foreach (var modifier in InputModifier)
        {
            modifier.OnMove -= Move;
        }
    }

    // Returns the modified Input State
    public InputState GetState()
    {
        InputState _state;
        _state.MovementDirection = Vector2.zero;

        // Iterate through modifiers befure returning the final Input State
        foreach (var modifier in InputModifier)
        {
            modifier.Process(ref _state);
        }

        return _state;
    }

    private void Move(Vector2 direction)
    {
        OnMove?.Invoke(GetState().MovementDirection);
    }

    private void Jump()
    {
        if (!GetState().CanJump)
            return;

        OnJump?.Invoke();
    }

    private void Roll()
    {
        if (!GetState().CanRoll)
            return;

        OnRoll?.Invoke();
    }

    private void Attack()
    {
        if (!GetState().CanAttack)
            return;

        OnAttack?.Invoke();
    }
}