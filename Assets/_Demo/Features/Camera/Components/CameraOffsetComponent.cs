using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(EventTarget.Self)]
public sealed class CameraOffsetComponent : IComponent
{
    public Vector3 Position;
    public float Zoom;
}