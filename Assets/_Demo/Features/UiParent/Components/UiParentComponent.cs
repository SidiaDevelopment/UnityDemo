using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique]
public sealed class UiParentComponent : IComponent
{
    public Transform Value;
}