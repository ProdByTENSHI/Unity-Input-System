// Base Class for all Scripts that want to modify the Input State in the Input Provider
using System;
using UnityEngine;

public abstract class InputModifier : MonoBehaviour
{
    public Action<Vector2> OnMove;
    
    public abstract void Process(ref InputState state);
}