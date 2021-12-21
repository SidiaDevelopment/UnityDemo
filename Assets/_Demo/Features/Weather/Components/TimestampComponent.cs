using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public sealed class TimestampComponent : IComponent
{
    public long Value;
}