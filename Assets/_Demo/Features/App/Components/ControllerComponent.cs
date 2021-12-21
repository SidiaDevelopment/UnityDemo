using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique]
public sealed class ControllerComponent : IComponent
{
    public MonoBehaviour Value;
}