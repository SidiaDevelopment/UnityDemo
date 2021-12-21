using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(EventTarget.Self)]
public sealed class PlaceableRotationComponent : IComponent
{
    public Vector3 Value;
}