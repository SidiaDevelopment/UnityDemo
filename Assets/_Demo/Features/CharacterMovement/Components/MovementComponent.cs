using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique]
public sealed class MovementComponent : IComponent
{
    public Vector2 Value;
}