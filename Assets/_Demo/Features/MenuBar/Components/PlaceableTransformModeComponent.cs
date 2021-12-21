using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public sealed class PlaceableTransformModeComponent : IComponent
{
    public TransformMode Value;
}

public enum TransformMode
{
    Position,
    Scale,
    Rotation
}